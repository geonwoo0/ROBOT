using RobotCore;

namespace RobotMoveL
{
    // Linear move command payload.
    public class MoveLCommand
    {
        public float X;
        public float Y;
        public float Z;
        public float Q0Degree;
        public float Speed;
        public float Acc;
        public float Dec;
    }

    public class MoveLExecutor
    {
        private readonly ModbusHelper modbus;

        public MoveLExecutor(ModbusHelper modbusHelper)
        {
            modbus = modbusHelper;
        }

        public void Execute(RcPointInformation rcpath, MoveLCommand cmd, uint pointNumber = 1, int maxRegsPerWrite = 100)
        {
            // Handshake registers for controller command trigger.
            modbus.WriteRegister("D100", 1);
            modbus.WriteCoil("M10", true);

            // Cartesian target + converted Q0 orientation.
            rcpath.TargetPos.Pos.X = cmd.X;
            rcpath.TargetPos.Pos.Y = cmd.Y;
            rcpath.TargetPos.Pos.Z = cmd.Z;
            rcpath.TargetPos.Orient.Q0 = QuaternionHelper.Q0FromDegrees(cmd.Q0Degree);
            rcpath.TargetPos.Orient.Q1 = 0;
            rcpath.TargetPos.Orient.Q2 = 0;
            rcpath.TargetPos.Orient.Q3 = 0;
            rcpath.RefCoordSystem = 0;

            rcpath.MaxSpeedPercent = cmd.Speed;
            rcpath.MaxAccPercent = cmd.Acc;
            rcpath.MaxDecPercent = cmd.Dec;
            rcpath.PointNumber = pointNumber;

            // Write full point packet then pulse execute bit.
            ushort[] regs = RcPointModbusCodec.ToRegisters(rcpath, wordOrder32: WordOrder.Swap);
            modbus.WriteRegistersChunked("D1000", regs, maxRegsPerWrite);
            modbus.WriteCoil("SM3160", true);
        }
    }
}
