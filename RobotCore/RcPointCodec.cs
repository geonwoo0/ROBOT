using System;

namespace RobotCore
{
    // Basic pose structs used by RcPointInformation.
    public class RbStructPos { public float X; public float Y; public float Z; }
    public class RbStructOrient { public float Q0; public float Q1; public float Q2; public float Q3; }
    public class CoordPos { public RbStructPos Pos = new RbStructPos(); public RbStructOrient Orient = new RbStructOrient(); }

    // Robot controller RCPATH point packet model (mapped to D0~D99).
    public class RcPointInformation
    {
        public CoordPos TargetPos = new CoordPos();
        public ushort ArcAngle_x100; public ushort ToolId; public ushort Handstrap; public short TransitionParam;
        public ushort RefCoordSystem; public ushort WorkpieceNumber;
        public float MaxSpeedPercent; public float MaxAccPercent; public float MaxDecPercent; public float PathTransErrPct;
        public ushort PathType; public ushort PtpPlanType; public short Cf1; public short Cf4; public short Cf6; public ushort Cfx;
        public ushort ExtAxisMotionType; public ushort ExtAxisTransParam; public float CpTransByLength;
        public ushort D38_Obligate; public ushort D39_Obligate;
        public float ExtShaft1Angle; public float ExtShaft2Angle; public float ExtShaft3Disp; public float ExtShaftSpeedPct; public float ExtShaftAccPct;
        public ushort ArcFollowPose; public ushort SwingMode; public float BendArcLength; public float BendArcRange; public float SwingHeight;
        public float LeftRetentionLen; public float RetentionTime; public float RightRetentionLen;
        public ushort D64_Obligate; public ushort D65_Obligate; public ushort D66_Obligate; public ushort D67_Obligate;
        public float IntersectionEndConnectLen; public uint SenseMOffset; public uint TillMOffset; public uint FindMOffset;
        public ushort JumpTrajectoryType; public ushort JumpCarryForwardCp; public float ArchS; public float ArchE; public float LimZ;
        public uint PointNumber; public ushort AttitudeTransPointCount; public ushort WaitTimeMs; public int WaitMSignalSeq;
        public ushort ProcessOut1Serial; public ushort ProcessOut2Serial; public float ExternalShaftLiftHeight;
        public ushort WristSingularAvoid; public ushort FollowMotionType; public ushort ChaseGrabCount;
        public ushort D97_Obligate; public ushort D98_Obligate; public ushort D99_Obligate;
    }

    public enum WordOrder { Normal, Swap }

    public static class QuaternionHelper
    {
        // Convert single-axis degree input to quaternion scalar term (Q0).
        public static float Q0FromDegrees(float degree)
        {
            double halfRad = degree * Math.PI / 360.0;
            return (float)Math.Cos(halfRad);
        }
    }

    public static class RcPointModbusCodec
    {
        // Encode RcPointInformation to 100 holding registers.
        public static ushort[] ToRegisters(RcPointInformation p, WordOrder wordOrder32 = WordOrder.Normal)
        {
            var regs = new ushort[100];
            void SetU16(int d, ushort v) { if (d >= 0 && d < regs.Length) regs[d] = v; }
            void SetS16(int d, short v) => SetU16(d, unchecked((ushort)v));
            void Set2Words(int d, ushort high, ushort low)
            {
                if (d < 0 || d + 1 >= regs.Length) return;
                // Some PLCs require swapped 16-bit word order for 32-bit values.
                if (wordOrder32 == WordOrder.Swap) { regs[d] = high; regs[d + 1] = low; }
                else { regs[d] = low; regs[d + 1] = high; }
            }
            void SetF32(int d, float v) { var b = BitConverter.GetBytes(v); Set2Words(d, BitConverter.ToUInt16(b, 0), BitConverter.ToUInt16(b, 2)); }
            void SetU32(int d, uint v) { var b = BitConverter.GetBytes(v); Set2Words(d, BitConverter.ToUInt16(b, 0), BitConverter.ToUInt16(b, 2)); }
            void SetS32(int d, int v) => SetU32(d, unchecked((uint)v));

            SetF32(0, p.TargetPos.Pos.X); SetF32(2, p.TargetPos.Pos.Y); SetF32(4, p.TargetPos.Pos.Z);
            SetF32(6, p.TargetPos.Orient.Q0); SetF32(8, p.TargetPos.Orient.Q1); SetF32(10, p.TargetPos.Orient.Q2); SetF32(12, p.TargetPos.Orient.Q3);
            SetU16(14, p.ArcAngle_x100); SetU16(15, p.ToolId); SetU16(16, p.Handstrap); SetS16(17, p.TransitionParam); SetU16(18, p.RefCoordSystem); SetU16(19, p.WorkpieceNumber);
            SetF32(20, p.MaxSpeedPercent); SetF32(22, p.MaxAccPercent); SetF32(24, p.MaxDecPercent); SetF32(26, p.PathTransErrPct);
            SetU16(28, p.PathType); SetU16(29, p.PtpPlanType); SetS16(30, p.Cf1); SetS16(31, p.Cf4); SetS16(32, p.Cf6); SetU16(33, p.Cfx);
            SetU16(34, p.ExtAxisMotionType); SetU16(35, p.ExtAxisTransParam); SetF32(36, p.CpTransByLength);
            SetU16(38, p.D38_Obligate); SetU16(39, p.D39_Obligate);
            SetF32(40, p.ExtShaft1Angle); SetF32(42, p.ExtShaft2Angle); SetF32(44, p.ExtShaft3Disp); SetF32(46, p.ExtShaftSpeedPct); SetF32(48, p.ExtShaftAccPct);
            SetU16(50, p.ArcFollowPose); SetU16(51, p.SwingMode); SetF32(52, p.BendArcLength); SetF32(54, p.BendArcRange); SetF32(56, p.SwingHeight); SetF32(58, p.LeftRetentionLen);
            SetF32(60, p.RetentionTime); SetF32(62, p.RightRetentionLen);
            SetU16(64, p.D64_Obligate); SetU16(65, p.D65_Obligate); SetU16(66, p.D66_Obligate); SetU16(67, p.D67_Obligate);
            SetF32(68, p.IntersectionEndConnectLen); SetU32(70, p.SenseMOffset); SetU32(72, p.TillMOffset); SetU32(74, p.FindMOffset);
            SetU16(76, p.JumpTrajectoryType); SetU16(77, p.JumpCarryForwardCp); SetF32(78, p.ArchS); SetF32(80, p.ArchE); SetF32(82, p.LimZ);
            SetU32(84, p.PointNumber); SetU16(86, p.AttitudeTransPointCount); SetU16(87, p.WaitTimeMs); SetS32(88, p.WaitMSignalSeq);
            SetU16(90, p.ProcessOut1Serial); SetU16(91, p.ProcessOut2Serial); SetF32(92, p.ExternalShaftLiftHeight); SetU16(94, p.WristSingularAvoid); SetU16(95, p.FollowMotionType); SetU16(96, p.ChaseGrabCount);
            SetU16(97, p.D97_Obligate); SetU16(98, p.D98_Obligate); SetU16(99, p.D99_Obligate);
            return regs;
        }
    }
}
