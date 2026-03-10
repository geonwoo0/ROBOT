namespace RobotTest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.RBON = new System.Windows.Forms.Button();
            this.RBSTOP = new System.Windows.Forms.Button();
            this.posX = new System.Windows.Forms.TextBox();
            this.posY = new System.Windows.Forms.TextBox();
            this.posZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RBOFF = new System.Windows.Forms.Button();
            this.ESTOP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.posR = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.posJ4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.posJ3 = new System.Windows.Forms.TextBox();
            this.posJ2 = new System.Windows.Forms.TextBox();
            this.posJ1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtJ4 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtJ3 = new System.Windows.Forms.TextBox();
            this.txtJ2 = new System.Windows.Forms.TextBox();
            this.txtJ1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtDEC = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtACC = new System.Windows.Forms.TextBox();
            this.txtSPEED = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtR = new System.Windows.Forms.TextBox();
            this.btn_move = new System.Windows.Forms.Button();
            this.chk_MoveJ = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_errrst = new System.Windows.Forms.Button();
            this.btn_j1down = new System.Windows.Forms.Button();
            this.btn_j1up = new System.Windows.Forms.Button();
            this.btn_j4up = new System.Windows.Forms.Button();
            this.btn_j4down = new System.Windows.Forms.Button();
            this.btn_j3up = new System.Windows.Forms.Button();
            this.btn_j3down = new System.Windows.Forms.Button();
            this.btn_j2up = new System.Windows.Forms.Button();
            this.btn_j2down = new System.Windows.Forms.Button();
            this.list_move = new System.Windows.Forms.ListBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btnRunList = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_saveJson = new System.Windows.Forms.Button();
            this.btn_loadJson = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RBON
            // 
            this.RBON.Location = new System.Drawing.Point(12, 415);
            this.RBON.Name = "RBON";
            this.RBON.Size = new System.Drawing.Size(56, 23);
            this.RBON.TabIndex = 0;
            this.RBON.Text = "RBON";
            this.RBON.UseVisualStyleBackColor = true;
            this.RBON.Click += new System.EventHandler(this.RBON_Click);
            // 
            // RBSTOP
            // 
            this.RBSTOP.Location = new System.Drawing.Point(136, 415);
            this.RBSTOP.Name = "RBSTOP";
            this.RBSTOP.Size = new System.Drawing.Size(65, 23);
            this.RBSTOP.TabIndex = 1;
            this.RBSTOP.Text = "RBSTOP";
            this.RBSTOP.UseVisualStyleBackColor = true;
            this.RBSTOP.Click += new System.EventHandler(this.RBSTOP_Click);
            // 
            // posX
            // 
            this.posX.Location = new System.Drawing.Point(511, 10);
            this.posX.Name = "posX";
            this.posX.Size = new System.Drawing.Size(100, 21);
            this.posX.TabIndex = 2;
            // 
            // posY
            // 
            this.posY.Location = new System.Drawing.Point(511, 37);
            this.posY.Name = "posY";
            this.posY.Size = new System.Drawing.Size(100, 21);
            this.posY.TabIndex = 3;
            // 
            // posZ
            // 
            this.posZ.Location = new System.Drawing.Point(511, 64);
            this.posZ.Name = "posZ";
            this.posZ.Size = new System.Drawing.Size(100, 21);
            this.posZ.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(478, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "X :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(478, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(478, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Z :";
            // 
            // RBOFF
            // 
            this.RBOFF.Location = new System.Drawing.Point(74, 415);
            this.RBOFF.Name = "RBOFF";
            this.RBOFF.Size = new System.Drawing.Size(56, 23);
            this.RBOFF.TabIndex = 9;
            this.RBOFF.Text = "RBOFF";
            this.RBOFF.UseVisualStyleBackColor = true;
            this.RBOFF.Click += new System.EventHandler(this.RBOFF_Click);
            // 
            // ESTOP
            // 
            this.ESTOP.BackColor = System.Drawing.Color.DarkRed;
            this.ESTOP.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ESTOP.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ESTOP.Location = new System.Drawing.Point(657, 415);
            this.ESTOP.Name = "ESTOP";
            this.ESTOP.Size = new System.Drawing.Size(143, 34);
            this.ESTOP.TabIndex = 10;
            this.ESTOP.Text = "E-STOP";
            this.ESTOP.UseVisualStyleBackColor = false;
            this.ESTOP.Click += new System.EventHandler(this.ESTOP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(655, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "Z :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(655, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "Y :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(655, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "X :";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(688, 64);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(100, 21);
            this.txtZ.TabIndex = 13;
            this.txtZ.Text = "0";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(688, 37);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 21);
            this.txtY.TabIndex = 12;
            this.txtY.Text = "0";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(688, 10);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 21);
            this.txtX.TabIndex = 11;
            this.txtX.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(478, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "R :";
            // 
            // posR
            // 
            this.posR.Location = new System.Drawing.Point(511, 91);
            this.posR.Name = "posR";
            this.posR.Size = new System.Drawing.Size(100, 21);
            this.posR.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(473, 206);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 12);
            this.label13.TabIndex = 36;
            this.label13.Text = "J4 :";
            // 
            // posJ4
            // 
            this.posJ4.Location = new System.Drawing.Point(511, 203);
            this.posJ4.Name = "posJ4";
            this.posJ4.Size = new System.Drawing.Size(85, 21);
            this.posJ4.TabIndex = 35;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(473, 179);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 12);
            this.label14.TabIndex = 34;
            this.label14.Text = "J3 :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(473, 152);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 12);
            this.label15.TabIndex = 33;
            this.label15.Text = "J2 :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(473, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 12);
            this.label16.TabIndex = 32;
            this.label16.Text = "J1 :";
            // 
            // posJ3
            // 
            this.posJ3.Location = new System.Drawing.Point(511, 176);
            this.posJ3.Name = "posJ3";
            this.posJ3.Size = new System.Drawing.Size(85, 21);
            this.posJ3.TabIndex = 31;
            // 
            // posJ2
            // 
            this.posJ2.Location = new System.Drawing.Point(511, 149);
            this.posJ2.Name = "posJ2";
            this.posJ2.Size = new System.Drawing.Size(85, 21);
            this.posJ2.TabIndex = 30;
            // 
            // posJ1
            // 
            this.posJ1.Location = new System.Drawing.Point(511, 122);
            this.posJ1.Name = "posJ1";
            this.posJ1.Size = new System.Drawing.Size(85, 21);
            this.posJ1.TabIndex = 29;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(650, 206);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 12);
            this.label17.TabIndex = 56;
            this.label17.Text = "J4 :";
            // 
            // txtJ4
            // 
            this.txtJ4.Location = new System.Drawing.Point(688, 203);
            this.txtJ4.Name = "txtJ4";
            this.txtJ4.Size = new System.Drawing.Size(100, 21);
            this.txtJ4.TabIndex = 55;
            this.txtJ4.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(650, 179);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 12);
            this.label18.TabIndex = 54;
            this.label18.Text = "J3 :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(650, 152);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(25, 12);
            this.label19.TabIndex = 53;
            this.label19.Text = "J2 :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(650, 125);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(25, 12);
            this.label20.TabIndex = 52;
            this.label20.Text = "J1 :";
            // 
            // txtJ3
            // 
            this.txtJ3.Location = new System.Drawing.Point(688, 176);
            this.txtJ3.Name = "txtJ3";
            this.txtJ3.Size = new System.Drawing.Size(100, 21);
            this.txtJ3.TabIndex = 51;
            this.txtJ3.Text = "0";
            // 
            // txtJ2
            // 
            this.txtJ2.Location = new System.Drawing.Point(688, 149);
            this.txtJ2.Name = "txtJ2";
            this.txtJ2.Size = new System.Drawing.Size(100, 21);
            this.txtJ2.TabIndex = 50;
            this.txtJ2.Text = "0";
            // 
            // txtJ1
            // 
            this.txtJ1.Location = new System.Drawing.Point(688, 122);
            this.txtJ1.Name = "txtJ1";
            this.txtJ1.Size = new System.Drawing.Size(100, 21);
            this.txtJ1.TabIndex = 49;
            this.txtJ1.Text = "0";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(637, 287);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 12);
            this.label22.TabIndex = 46;
            this.label22.Text = "DEC :";
            // 
            // txtDEC
            // 
            this.txtDEC.Location = new System.Drawing.Point(688, 284);
            this.txtDEC.Name = "txtDEC";
            this.txtDEC.Size = new System.Drawing.Size(100, 21);
            this.txtDEC.TabIndex = 45;
            this.txtDEC.Text = "30";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(637, 260);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 12);
            this.label23.TabIndex = 44;
            this.label23.Text = "ACC :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(623, 233);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 43;
            this.label24.Text = "SPEED :";
            // 
            // txtACC
            // 
            this.txtACC.Location = new System.Drawing.Point(688, 257);
            this.txtACC.Name = "txtACC";
            this.txtACC.Size = new System.Drawing.Size(100, 21);
            this.txtACC.TabIndex = 41;
            this.txtACC.Text = "30";
            // 
            // txtSPEED
            // 
            this.txtSPEED.Location = new System.Drawing.Point(688, 230);
            this.txtSPEED.Name = "txtSPEED";
            this.txtSPEED.Size = new System.Drawing.Size(100, 21);
            this.txtSPEED.TabIndex = 40;
            this.txtSPEED.Text = "15";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(655, 94);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(52, 12);
            this.label26.TabIndex = 38;
            this.label26.Text = "Q0(deg):";
            // 
            // txtR
            // 
            this.txtR.Location = new System.Drawing.Point(688, 91);
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(100, 21);
            this.txtR.TabIndex = 37;
            this.txtR.Text = "0";
            // 
            // btn_move
            // 
            this.btn_move.Location = new System.Drawing.Point(657, 367);
            this.btn_move.Name = "btn_move";
            this.btn_move.Size = new System.Drawing.Size(131, 29);
            this.btn_move.TabIndex = 57;
            this.btn_move.Text = "MOVE";
            this.btn_move.UseVisualStyleBackColor = true;
            this.btn_move.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_MoveJ
            // 
            this.chk_MoveJ.AutoSize = true;
            this.chk_MoveJ.Location = new System.Drawing.Point(702, 313);
            this.chk_MoveJ.Name = "chk_MoveJ";
            this.chk_MoveJ.Size = new System.Drawing.Size(92, 21);
            this.chk_MoveJ.TabIndex = 58;
            this.chk_MoveJ.Text = "Joint Move";
            this.chk_MoveJ.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(637, 338);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 24);
            this.label8.TabIndex = 59;
            this.label8.Text = "Joint Move 체크 시 \r\nJ1,J2,J3,J4 값의 각도로 이동";
            // 
            // btn_errrst
            // 
            this.btn_errrst.Location = new System.Drawing.Point(207, 415);
            this.btn_errrst.Name = "btn_errrst";
            this.btn_errrst.Size = new System.Drawing.Size(75, 23);
            this.btn_errrst.TabIndex = 60;
            this.btn_errrst.Text = "ERR RST";
            this.btn_errrst.UseVisualStyleBackColor = true;
            this.btn_errrst.Click += new System.EventHandler(this.btn_errrst_Click);
            // 
            // btn_j1down
            // 
            this.btn_j1down.Location = new System.Drawing.Point(602, 120);
            this.btn_j1down.Name = "btn_j1down";
            this.btn_j1down.Size = new System.Drawing.Size(19, 23);
            this.btn_j1down.TabIndex = 61;
            this.btn_j1down.Text = "-";
            this.btn_j1down.UseVisualStyleBackColor = true;
            // 
            // btn_j1up
            // 
            this.btn_j1up.Location = new System.Drawing.Point(624, 120);
            this.btn_j1up.Name = "btn_j1up";
            this.btn_j1up.Size = new System.Drawing.Size(19, 23);
            this.btn_j1up.TabIndex = 62;
            this.btn_j1up.Text = "+";
            this.btn_j1up.UseVisualStyleBackColor = true;
            // 
            // btn_j4up
            // 
            this.btn_j4up.Location = new System.Drawing.Point(624, 201);
            this.btn_j4up.Name = "btn_j4up";
            this.btn_j4up.Size = new System.Drawing.Size(19, 23);
            this.btn_j4up.TabIndex = 64;
            this.btn_j4up.Text = "+";
            this.btn_j4up.UseVisualStyleBackColor = true;
            // 
            // btn_j4down
            // 
            this.btn_j4down.Location = new System.Drawing.Point(602, 201);
            this.btn_j4down.Name = "btn_j4down";
            this.btn_j4down.Size = new System.Drawing.Size(19, 23);
            this.btn_j4down.TabIndex = 63;
            this.btn_j4down.Text = "-";
            this.btn_j4down.UseVisualStyleBackColor = true;
            // 
            // btn_j3up
            // 
            this.btn_j3up.Location = new System.Drawing.Point(624, 174);
            this.btn_j3up.Name = "btn_j3up";
            this.btn_j3up.Size = new System.Drawing.Size(19, 23);
            this.btn_j3up.TabIndex = 66;
            this.btn_j3up.Text = "+";
            this.btn_j3up.UseVisualStyleBackColor = true;
            // 
            // btn_j3down
            // 
            this.btn_j3down.Location = new System.Drawing.Point(602, 174);
            this.btn_j3down.Name = "btn_j3down";
            this.btn_j3down.Size = new System.Drawing.Size(19, 23);
            this.btn_j3down.TabIndex = 65;
            this.btn_j3down.Text = "-";
            this.btn_j3down.UseVisualStyleBackColor = true;
            // 
            // btn_j2up
            // 
            this.btn_j2up.Location = new System.Drawing.Point(624, 147);
            this.btn_j2up.Name = "btn_j2up";
            this.btn_j2up.Size = new System.Drawing.Size(19, 23);
            this.btn_j2up.TabIndex = 68;
            this.btn_j2up.Text = "+";
            this.btn_j2up.UseVisualStyleBackColor = true;
            // 
            // btn_j2down
            // 
            this.btn_j2down.Location = new System.Drawing.Point(602, 147);
            this.btn_j2down.Name = "btn_j2down";
            this.btn_j2down.Size = new System.Drawing.Size(19, 23);
            this.btn_j2down.TabIndex = 67;
            this.btn_j2down.Text = "-";
            this.btn_j2down.UseVisualStyleBackColor = true;
            // 
            // list_move
            // 
            this.list_move.FormattingEnabled = true;
            this.list_move.ItemHeight = 12;
            this.list_move.Location = new System.Drawing.Point(12, 13);
            this.list_move.Name = "list_move";
            this.list_move.Size = new System.Drawing.Size(189, 220);
            this.list_move.TabIndex = 69;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(207, 13);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 70;
            this.btn_add.Text = "ADD";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btnRunList
            // 
            this.btnRunList.Location = new System.Drawing.Point(207, 42);
            this.btnRunList.Name = "btnRunList";
            this.btnRunList.Size = new System.Drawing.Size(75, 23);
            this.btnRunList.TabIndex = 71;
            this.btnRunList.Text = "RUN";
            this.btnRunList.UseVisualStyleBackColor = true;
            this.btnRunList.Click += new System.EventHandler(this.btnRunList_Click_1);
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(207, 71);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_remove.TabIndex = 72;
            this.btn_remove.Text = "DEL";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_saveJson
            // 
            this.btn_saveJson.Location = new System.Drawing.Point(207, 100);
            this.btn_saveJson.Name = "btn_saveJson";
            this.btn_saveJson.Size = new System.Drawing.Size(90, 23);
            this.btn_saveJson.TabIndex = 73;
            this.btn_saveJson.Text = "SAVE JSON";
            this.btn_saveJson.UseVisualStyleBackColor = true;
            this.btn_saveJson.Click += new System.EventHandler(this.btn_saveJson_Click);
            // 
            // btn_loadJson
            // 
            this.btn_loadJson.Location = new System.Drawing.Point(207, 129);
            this.btn_loadJson.Name = "btn_loadJson";
            this.btn_loadJson.Size = new System.Drawing.Size(90, 23);
            this.btn_loadJson.TabIndex = 74;
            this.btn_loadJson.Text = "LOAD JSON";
            this.btn_loadJson.UseVisualStyleBackColor = true;
            this.btn_loadJson.Click += new System.EventHandler(this.btn_loadJson_Click);
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 455);
            this.Controls.Add(this.btn_loadJson);
            this.Controls.Add(this.btn_saveJson);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btnRunList);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.list_move);
            this.Controls.Add(this.btn_j2up);
            this.Controls.Add(this.btn_j2down);
            this.Controls.Add(this.btn_j3up);
            this.Controls.Add(this.btn_j3down);
            this.Controls.Add(this.btn_j4up);
            this.Controls.Add(this.btn_j4down);
            this.Controls.Add(this.btn_j1up);
            this.Controls.Add(this.btn_j1down);
            this.Controls.Add(this.btn_errrst);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chk_MoveJ);
            this.Controls.Add(this.btn_move);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtJ4);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtJ3);
            this.Controls.Add(this.txtJ2);
            this.Controls.Add(this.txtJ1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtDEC);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtACC);
            this.Controls.Add(this.txtSPEED);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtR);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.posJ4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.posJ3);
            this.Controls.Add(this.posJ2);
            this.Controls.Add(this.posJ1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.posR);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtZ);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.ESTOP);
            this.Controls.Add(this.RBOFF);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.posZ);
            this.Controls.Add(this.posY);
            this.Controls.Add(this.posX);
            this.Controls.Add(this.RBSTOP);
            this.Controls.Add(this.RBON);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RBON;
        private System.Windows.Forms.Button RBSTOP;
        private System.Windows.Forms.TextBox posX;
        private System.Windows.Forms.TextBox posY;
        private System.Windows.Forms.TextBox posZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RBOFF;
        private System.Windows.Forms.Button ESTOP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox posR;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox posJ4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox posJ3;
        private System.Windows.Forms.TextBox posJ2;
        private System.Windows.Forms.TextBox posJ1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtJ4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtJ3;
        private System.Windows.Forms.TextBox txtJ2;
        private System.Windows.Forms.TextBox txtJ1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtDEC;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtACC;
        private System.Windows.Forms.TextBox txtSPEED;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtR;
        private System.Windows.Forms.Button btn_move;
        private System.Windows.Forms.CheckBox chk_MoveJ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_errrst;
        private System.Windows.Forms.Button btn_j1down;
        private System.Windows.Forms.Button btn_j1up;
        private System.Windows.Forms.Button btn_j4up;
        private System.Windows.Forms.Button btn_j4down;
        private System.Windows.Forms.Button btn_j3up;
        private System.Windows.Forms.Button btn_j3down;
        private System.Windows.Forms.Button btn_j2up;
        private System.Windows.Forms.Button btn_j2down;
        private System.Windows.Forms.ListBox list_move;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btnRunList;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_saveJson;
        private System.Windows.Forms.Button btn_loadJson;
    }
}

