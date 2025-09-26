using System.IO.Ports;
using Modbus.Device;

namespace ProductionMonitoringPlatform.Services;

public class ModebusServer
{
    public void GetEnvironmentModelData()
    {
        ushort startAddr = 0;
        ushort quantity = 7;

        // var command = new List<byte>();
        // command.Add(0x01); // 地址码01
        // command.Add(0x01); // 功能码01
        //
        // command.Add(BitConverter.GetBytes(startAddr)[1]); // 起始地址高位
        // command.Add(BitConverter.GetBytes(startAddr)[0]); // 起始地址低位
        //
        // command.Add(BitConverter.GetBytes(quantity)[1]); // 读取数量高位
        // command.Add(BitConverter.GetBytes(quantity)[0]); // 读取数量低位
        //
        // var crcBytes = CRC16(command.ToArray());
        // command.Add(crcBytes[0]); // CRC低位
        // command.Add(crcBytes[1]); // CRC高位
        //
        // serialPort.Open();
        // serialPort.Write(command.ToArray(), 0, command.Count);
        //
        // var respBytes = new byte[serialPort.BytesToRead];
        // serialPort.Read(respBytes, 0, respBytes.Length);
        //
        // var respList = new List<byte>(respBytes);
        // if (respList.Count < 3)
        // {
        //     Console.WriteLine("No info");
        //     return;
        // }
        // respList.RemoveRange(0, 3); // 设备地址 功能码 数据字节计数
        // respList.RemoveRange(respList.Count - 2, 2); // CRC校验码占用最后2个字节
        // respList.Reverse(); // 反转字节顺序 地位考前
        //
        // var respStrList = respList.Select(b => Convert.ToString(b, 2)).ToList(); // 转换为二进制字符串
        // var res = respStrList.Aggregate(
        //     string.Empty,
        //     (current, item) => current + item.PadLeft(8, '0')
        // ); // 将所有8位二进制字符串连接成一个长字符串
        //
        // res = new string(res.ToArray().Reverse().ToArray());
        // res = res.Length > quantity ? res.Substring(0, quantity) : res;
        // Console.WriteLine(res);

        using var serialPort = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);
        serialPort.Open();
        var master = ModbusSerialMaster.CreateRtu(serialPort);
        var res = master.ReadCoils(1, startAddr, quantity);
        Console.WriteLine(string.Join(",", res));
    }
}
