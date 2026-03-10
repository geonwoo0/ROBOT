using System;
using System.Net.Sockets;
using NModbus;

namespace RobotCore
{
    // Thin wrapper around NModbus with project-specific address mapping.
    public class ModbusHelper
    {
        private TcpClient tcpClient;
        private IModbusMaster master;
        private byte slaveId = 1;

        public void Connect(string ip, int port)
        {
            // Open TCP socket and create Modbus master.
            tcpClient = new TcpClient(ip, port);
            var factory = new ModbusFactory();
            master = factory.CreateMaster(tcpClient);
        }

        public void Disconnect() { tcpClient?.Close(); }
        // Coil/Register write helpers
        public void WriteCoil(string addr, bool value) { master.WriteSingleCoil(slaveId, ConvertAddress(addr), value); }
        public void WriteRegister(string addr, ushort value) { master.WriteSingleRegister(slaveId, ConvertAddress(addr), value); }
        public void WriteRegisters(string addr, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            master.WriteMultipleRegisters(slaveId, ConvertAddress(addr), new ushort[] { BitConverter.ToUInt16(bytes, 0), BitConverter.ToUInt16(bytes, 2) });
        }
        public void WriteFloat(string addr, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            master.WriteMultipleRegisters(slaveId, ConvertAddress(addr), new ushort[] { BitConverter.ToUInt16(bytes, 0), BitConverter.ToUInt16(bytes, 2) });
        }
        // Coil/Register read helpers
        public bool ReadCoil(string addr) { return master.ReadCoils(slaveId, ConvertAddress(addr), 1)[0]; }
        public bool[] ReadCoils(string addr, ushort count) { return master.ReadCoils(slaveId, ConvertAddress(addr), count); }
        public ushort ReadRegister(string addr) { return master.ReadHoldingRegisters(slaveId, ConvertAddress(addr), 1)[0]; }
        public ushort[] ReadRegisters(string addr, ushort count) { return master.ReadHoldingRegisters(slaveId, ConvertAddress(addr), count); }
        public float ReadFloat(string addr)
        {
            // Read two 16-bit registers and convert to float.
            ushort[] registers = master.ReadHoldingRegisters(slaveId, ConvertAddress(addr), 2);
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(registers[0] >> 8); bytes[1] = (byte)(registers[0] & 0xFF);
            bytes[2] = (byte)(registers[1] >> 8); bytes[3] = (byte)(registers[1] & 0xFF);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }
        public ushort ConvertAddress(string address)
        {
            // Project PLC map: SD/SM have base offsets, D/M are direct.
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("주소가 비어있습니다.");
            address = address.ToUpper().Trim();
            if (address.StartsWith("SD")) return (ushort)(28673 + int.Parse(address.Substring(2)));
            if (address.StartsWith("SM")) return (ushort)(36864 + int.Parse(address.Substring(2)));
            if (address.StartsWith("D")) return (ushort)int.Parse(address.Substring(1));
            if (address.StartsWith("M")) return (ushort)int.Parse(address.Substring(1));
            throw new ArgumentException("지원하지 않는 주소 형식");
        }
        public void WriteRegistersChunked(string startAddr, ushort[] regs, int maxRegsPerWrite = 100)
        {
            // Split long register writes to avoid PLC write-length limits.
            ushort start = ConvertAddress(startAddr);
            int offset = 0;
            while (offset < regs.Length)
            {
                int n = Math.Min(maxRegsPerWrite, regs.Length - offset);
                ushort[] chunk = new ushort[n];
                Array.Copy(regs, offset, chunk, 0, n);
                master.WriteMultipleRegisters(slaveId, (ushort)(start + offset), chunk);
                offset += n;
            }
        }
    }
}
