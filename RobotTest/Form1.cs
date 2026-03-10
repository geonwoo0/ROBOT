using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RobotCore;
using RobotMoveJ;
using RobotMoveL;

namespace RobotTest
{
    public partial class Form1 : Form
    {
        // Robot communication and motion helpers
        private ModbusHelper modbus;
        private CancellationTokenSource _pollCts;
        private CancellationTokenSource _runCts;
        private RcPointInformation rcpath;
        private MoveJExecutor _moveJExecutor;
        private MoveLExecutor _moveLExecutor;

        // Runtime state flags
        private bool _isMoving;
        private bool _isConnected;
        private string _loadedJsonPath;
        private bool _isStop;
        private bool _isEstop;

        // In-memory waypoint list (always joint-based for list run/json run)
        private readonly List<JointPoint> _points = new List<JointPoint>();

        public Form1()
        {
            InitializeComponent();
            BindJogButtonEvents();
            TryInitializeRobot();
        }

        [DataContract]
        public class JointPoint
        {
            // Joint target angles
            [DataMember] public float J1 { get; set; }
            [DataMember] public float J2 { get; set; }
            [DataMember] public float J3 { get; set; }
            [DataMember] public float J4 { get; set; }
        }

        [DataContract]
        public class JointPointFile
        {
            // JSON root object
            [DataMember] public List<JointPoint> Points { get; set; } = new List<JointPoint>();
        }

        private void TryInitializeRobot()
        {
            try
            {
                // Connection/init errors are swallowed here so the form still opens.
                InitModbus();
                DefaultSetting();
                StartPolling();
                _isConnected = true;
            }
            catch (Exception ex)
            {
                _isConnected = false;
                MessageBox.Show("로봇 연결 실패: " + ex.Message + Environment.NewLine + "폼은 실행되며 JSON 기능은 사용 가능합니다.");
            }
        }

        private void InitModbus()
        {
            // Build helper instances once and connect to PLC.
            modbus = new ModbusHelper();
            rcpath = new RcPointInformation();
            _moveJExecutor = new MoveJExecutor(modbus);
            _moveLExecutor = new MoveLExecutor(modbus);
            modbus.Connect("192.168.31.101", 502);
        }

        private void DefaultSetting()
        {
            // Default startup pose + motion profile.
            rcpath.TargetPos.Pos.X = 600;
            rcpath.TargetPos.Pos.Y = 0;
            rcpath.TargetPos.Pos.Z = 0;
            rcpath.TargetPos.Orient.Q0 = 1;
            rcpath.TargetPos.Orient.Q1 = 0;
            rcpath.TargetPos.Orient.Q2 = 0;
            rcpath.TargetPos.Orient.Q3 = 0;

            rcpath.ArcAngle_x100 = 0;
            rcpath.ToolId = 0;
            rcpath.Handstrap = 0;
            rcpath.TransitionParam = 100;
            rcpath.RefCoordSystem = 0;
            rcpath.WorkpieceNumber = 0;

            rcpath.MaxSpeedPercent = 15;
            rcpath.MaxAccPercent = 30;
            rcpath.MaxDecPercent = 30;
            rcpath.PathTransErrPct = 100;
            rcpath.PathType = 3;
            rcpath.PtpPlanType = 1;
            rcpath.PointNumber = 1;

            ushort[] regs = RcPointModbusCodec.ToRegisters(rcpath, wordOrder32: WordOrder.Swap);
            modbus.WriteRegistersChunked("D1000", regs, maxRegsPerWrite: 100);
        }

        private bool EnsureConnected()
        {
            // Guard all command paths when PLC is disconnected.
            if (_isConnected) return true;
            MessageBox.Show("로봇이 연결되지 않았습니다.");
            return false;
        }

        private void StartPolling()
        {
            // Periodically read robot state and refresh UI position fields.
            _pollCts = new CancellationTokenSource();
            var token = _pollCts.Token;

            Task.Run(async () =>
            {
                float lastPosX = float.MinValue;
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        float posXValue = modbus.ReadFloat("SD4000");
                        float posYValue = modbus.ReadFloat("SD4002");
                        float posZValue = modbus.ReadFloat("SD4004");
                        float posRValue = modbus.ReadFloat("SD4006");

                        float posJ1Value = modbus.ReadFloat("SD4020");
                        float posJ2Value = modbus.ReadFloat("SD4022");
                        float posJ3Value = modbus.ReadFloat("SD4024");
                        float posJ4Value = modbus.ReadFloat("SD4026");

                        // Moving status bit used by run/move guards.
                        _isMoving = modbus.ReadCoil("SM3001");

                        if (lastPosX != posXValue)
                        {
                            lastPosX = posXValue;
                            BeginInvoke(new Action(() =>
                            {
                                posX.Text = posXValue.ToString("0.###");
                                posY.Text = posYValue.ToString("0.###");
                                posZ.Text = posZValue.ToString("0.###");
                                posR.Text = posRValue.ToString("0.###");
                                posJ1.Text = posJ1Value.ToString("0.###");
                                posJ2.Text = posJ2Value.ToString("0.###");
                                posJ3.Text = posJ3Value.ToString("0.###");
                                posJ4.Text = posJ4Value.ToString("0.###");
                            }));
                        }
                    }
                    catch
                    {
                    }

                    await Task.Delay(200, token);
                }
            }, token);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Stop background tasks and close TCP on form exit.
            _pollCts?.Cancel();
            _runCts?.Cancel();
            modbus?.Disconnect();
            base.OnFormClosing(e);
        }

        private void RBON_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            modbus.WriteCoil("M0", true);
        }

        private void RBOFF_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            modbus.WriteCoil("M0", false);
        }

        private void RBSTOP_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            _isStop = true;
            modbus.WriteCoil("M9998", true);
        }

        private void ESTOP_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            _isEstop = true;
            modbus.WriteCoil("M9999", !modbus.ReadCoil("M9999"));
        }

        private void btn_errrst_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            modbus.WriteCoil("SM3004", false);
            modbus.WriteCoil("SM3005", false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            if (_isMoving)
            {
                MessageBox.Show("로봇 이동 중");
                return;
            }

            float speed = float.Parse(txtSPEED.Text);
            float acc = float.Parse(txtACC.Text);
            float dec = float.Parse(txtDEC.Text);

            if (chk_MoveJ.Checked)
            {
                // Joint move uses J1~J4 input fields directly.
                var cmd = new MoveJCommand
                {
                    J1 = float.Parse(txtJ1.Text),
                    J2 = float.Parse(txtJ2.Text),
                    J3 = float.Parse(txtJ3.Text),
                    J4 = float.Parse(txtJ4.Text),
                    Speed = speed,
                    Acc = acc,
                    Dec = dec
                };
                _moveJExecutor.Execute(rcpath, cmd, pointNumber: 1);
            }
            else
            {
                // Linear move uses XYZ + Q0(deg) input fields.
                var cmd = new MoveLCommand
                {
                    X = float.Parse(txtX.Text),
                    Y = float.Parse(txtY.Text),
                    Z = float.Parse(txtZ.Text),
                    Q0Degree = float.Parse(txtR.Text),
                    Speed = speed,
                    Acc = acc,
                    Dec = dec
                };
                _moveLExecutor.Execute(rcpath, cmd, pointNumber: 1);
            }
        }

        private void SetJog(string addr, bool value)
        {
            // Shared jog helper for button down/up.
            if (!EnsureConnected()) return;
            modbus.WriteCoil(addr, value);
        }

        private void BindJogButtonEvents()
        {
            // Designer keeps jog buttons without MouseDown/MouseUp handlers, bind at runtime.
            btn_j1up.MouseDown += btn_j1up_MouseDown;
            btn_j1up.MouseUp += btn_j1up_MouseUp;
            btn_j1down.MouseDown += btn_j1down_MouseDown;
            btn_j1down.MouseUp += btn_j1down_MouseUp;
            btn_j2up.MouseDown += btn_j2up_MouseDown;
            btn_j2up.MouseUp += btn_j2up_MouseUp;
            btn_j2down.MouseDown += btn_j2down_MouseDown;
            btn_j2down.MouseUp += btn_j2down_MouseUp;
            btn_j3up.MouseDown += btn_j3up_MouseDown;
            btn_j3up.MouseUp += btn_j3up_MouseUp;
            btn_j3down.MouseDown += btn_j3down_MouseDown;
            btn_j3down.MouseUp += btn_j3down_MouseUp;
            btn_j4up.MouseDown += btn_j4up_MouseDown;
            btn_j4up.MouseUp += btn_j4up_MouseUp;
            btn_j4down.MouseDown += btn_j4down_MouseDown;
            btn_j4down.MouseUp += btn_j4down_MouseUp;
        }

        private void btn_j1up_MouseDown(object sender, EventArgs e) { SetJog("M100", true); }
        private void btn_j1up_MouseUp(object sender, EventArgs e) { SetJog("M100", false); }
        private void btn_j1down_MouseDown(object sender, EventArgs e) { SetJog("M101", true); }
        private void btn_j1down_MouseUp(object sender, EventArgs e) { SetJog("M101", false); }
        private void btn_j2up_MouseDown(object sender, EventArgs e) { SetJog("M102", true); }
        private void btn_j2up_MouseUp(object sender, EventArgs e) { SetJog("M102", false); }
        private void btn_j2down_MouseDown(object sender, EventArgs e) { SetJog("M103", true); }
        private void btn_j2down_MouseUp(object sender, EventArgs e) { SetJog("M103", false); }
        private void btn_j3up_MouseDown(object sender, EventArgs e) { SetJog("M104", true); }
        private void btn_j3up_MouseUp(object sender, EventArgs e) { SetJog("M104", false); }
        private void btn_j3down_MouseDown(object sender, EventArgs e) { SetJog("M105", true); }
        private void btn_j3down_MouseUp(object sender, EventArgs e) { SetJog("M105", false); }
        private void btn_j4up_MouseDown(object sender, EventArgs e) { SetJog("M106", true); }
        private void btn_j4up_MouseUp(object sender, EventArgs e) { SetJog("M106", false); }
        private void btn_j4down_MouseDown(object sender, EventArgs e) { SetJog("M107", true); }
        private void btn_j4down_MouseUp(object sender, EventArgs e) { SetJog("M107", false); }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;

            // Snapshot current joint values and append to run list.
            var p = new JointPoint
            {
                J1 = modbus.ReadFloat("SD4020"),
                J2 = modbus.ReadFloat("SD4022"),
                J3 = modbus.ReadFloat("SD4024"),
                J4 = modbus.ReadFloat("SD4026")
            };

            _points.Add(p);
            RefreshMoveList();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            int idx = list_move.SelectedIndex;
            if (idx < 0 || idx >= _points.Count) return;

            _points.RemoveAt(idx);
            RefreshMoveList();
        }

        private void RefreshMoveList()
        {
            // Keep list UI synced with internal _points list.
            list_move.Items.Clear();
            for (int i = 0; i < _points.Count; i++)
            {
                JointPoint p = _points[i];
                list_move.Items.Add($"{i + 1:000}: J1={p.J1:0.###}, J2={p.J2:0.###}, J3={p.J3:0.###}, J4={p.J4:0.###}");
            }
        }

        private async void btnRunList_Click_1(object sender, EventArgs e)
        {
            await RunPointsAsync(_points, "리스트가 비었습니다.");
        }

        private async Task RunPointsAsync(List<JointPoint> points, string emptyMessage)
        {
            if (!EnsureConnected()) return;
            if (points.Count == 0)
            {
                MessageBox.Show(emptyMessage);
                return;
            }
            if (_isMoving)
            {
                MessageBox.Show("로봇 이동 중");
                return;
            }

            _runCts?.Cancel();
            _runCts = new CancellationTokenSource();
            var ct = _runCts.Token;

            btnRunList.Enabled = false;
            try
            {
                // Execute sequentially and wait until motion bit turns off for each point.
                while (true)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        ct.ThrowIfCancellationRequested();
                        int idx = i;
                        BeginInvoke(new Action(() => list_move.SelectedIndex = idx < list_move.Items.Count ? idx : -1));

                        SendMoveJoint(points[i], (uint)(i + 1));
                        await WaitMoveDoneAsync(ct);
                        if (_isStop)
                        {
                            _isStop = false;
                            return;
                        }
                        if (_isEstop)
                        {
                            _isEstop = false;
                            return;
                        }
                    } 
                }
                //MessageBox.Show("순차 이동 완료");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("취소됨");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
            finally
            {
                btnRunList.Enabled = true;
            }
        }

        private void SendMoveJoint(JointPoint p, uint pointNumber)
        {
            // Speed/acc/dec always come from current UI values.
            float speed = float.Parse(txtSPEED.Text);
            float acc = float.Parse(txtACC.Text);
            float dec = float.Parse(txtDEC.Text);

            var cmd = new MoveJCommand
            {
                J1 = p.J1,
                J2 = p.J2,
                J3 = p.J3,
                J4 = p.J4,
                Speed = speed,
                Acc = acc,
                Dec = dec
            };
            _moveJExecutor.Execute(rcpath, cmd, pointNumber);
        }

        private async Task WaitMoveDoneAsync(CancellationToken ct)
        {
            // 1) wait for motion start bit (timeout-like loop)
            for (int i = 0; i < 5; i++)
            {
                ct.ThrowIfCancellationRequested();
                if (modbus.ReadCoil("SM3001")) break;
                await Task.Delay(100, ct);
            }

            // 2) wait for motion complete (bit off)
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                if (!modbus.ReadCoil("SM3001")) break;
                await Task.Delay(100, ct);
            }
        }

        private void btn_saveJson_Click(object sender, EventArgs e)
        {
            // Save current list as JSON (joint-only points).
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = "waypoints.json"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            var file = new JointPointFile { Points = new List<JointPoint>(_points) };
            using (var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
            {
                var serializer = new DataContractJsonSerializer(typeof(JointPointFile));
                serializer.WriteObject(fs, file);
            }

            MessageBox.Show("저장 완료: " + dialog.FileName);
        }

        private void btn_loadJson_Click(object sender, EventArgs e)
        {
            // Load JSON points into current list; RUN uses this same list.
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            JointPointFile file;
            using (var fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractJsonSerializer(typeof(JointPointFile));
                file = serializer.ReadObject(fs) as JointPointFile;
            }

            _points.Clear();
            if (file?.Points != null)
            {
                _points.AddRange(file.Points);
            }

            _loadedJsonPath = dialog.FileName;
            RefreshMoveList();
            MessageBox.Show("불러오기 완료: " + _loadedJsonPath);
        }
    }
}
