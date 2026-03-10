using RobotCore;

namespace RobotMoveJ
{
    // Joint move command payload.
    public class MoveJCommand
    {
        public float J1;
        public float J2;
        public float J3;
        public float J4;
        public float Speed;
        public float Acc;
        public float Dec;
    }

    public class MoveJExecutor
    {
        private readonly ModbusHelper modbus;

        public MoveJExecutor(ModbusHelper modbusHelper)
        {
            modbus = modbusHelper;
        }

        public void Execute(RcPointInformation rcpath, MoveJCommand cmd, uint pointNumber = 1, int maxRegsPerWrite = 100)
        {
            // Handshake registers for controller command trigger.
            modbus.WriteRegister("D100", 1);
            modbus.WriteCoil("M10", true);

            // Joint values are mapped to controller target fields in joint coordinate mode.
            rcpath.TargetPos.Pos.X = cmd.J1;
            rcpath.TargetPos.Pos.Y = cmd.J2;
            rcpath.TargetPos.Pos.Z = cmd.J3;
            rcpath.TargetPos.Orient.Q0 = cmd.J4;
            rcpath.RefCoordSystem = 5;

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
