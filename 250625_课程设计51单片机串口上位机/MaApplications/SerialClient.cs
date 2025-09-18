using System.IO;
using System.IO.Ports;
using System.Text;

namespace MaApplications;

internal class SerialClient
{
    public SerialClient(SerialPort serialPort)
    {
        _serialPort = serialPort;
    }

    public event Action<string> DataReceived;
    private readonly SerialPort _serialPort;
    private readonly MemoryStream _buffer = new();
    private readonly object _lock = new(); // 线程同步锁

    public static string[] GetPortsName() => SerialPort.GetPortNames();

    public static IEnumerable<StopBits> GetStopBits() => Enum.GetValues<StopBits>();

    public static IEnumerable<Parity> GetParity() => Enum.GetValues<Parity>();

    public static SerialPort CreatePorts(
        string portName,
        int baudRate = 9600,
        int dataBits = 8,
        StopBits stopBits = StopBits.One,
        Parity parity = Parity.None
    )
    {
        var mySerialPort = new SerialPort(portName);
        mySerialPort.BaudRate = baudRate;
        mySerialPort.DataBits = dataBits;
        mySerialPort.StopBits = stopBits;
        mySerialPort.Parity = parity;
        mySerialPort.Handshake = Handshake.None;
        return mySerialPort;
    }

    public static SerialClient CreateEmpty()
    {
        return null;
    }

    public void OpenClose(bool isOpen)
    {
        if (isOpen)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.DataReceived += DataReceivedHandler;
                _serialPort.Open();
            }
        }
        else
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.DataReceived -= DataReceivedHandler; // 取消订阅事件
                DataReceived = null;
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }
    }

    private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        if (sender is SerialPort { BytesToRead: > 0 } sp)
        {
            var info = new byte[sp.BytesToRead];
            sp.Read(info, 0, info.Length);
            _buffer.Write(info, 0, info.Length);
            ProcessBuffer();
            Thread.Sleep(500);
        }
    }

    private void ProcessBuffer()
    {
        var buffer = _buffer.ToArray();
        var offset = 0;
        while (offset < buffer.Length - 1)
        {
            var endIndex = -1;
            for (var i = offset; i < buffer.Length - 1; i++)
            {
                if (buffer[i] == ';')
                {
                    endIndex = i;
                    break;
                }
            }
            if (endIndex < 0)
                break;

            var frameLength = endIndex - offset;
            var frame = new byte[frameLength];
            Buffer.BlockCopy(buffer, offset, frame, 0, frameLength);

            var text = Encoding.ASCII.GetString(frame);
            if (!string.IsNullOrWhiteSpace(text))
            {
                OnDataReceived(text);
            }

            offset = endIndex + 1;
        }

        if (offset < buffer.Length)
        {
            var remaining = new byte[buffer.Length - offset];
            Buffer.BlockCopy(buffer, offset, remaining, 0, remaining.Length);
            _buffer.SetLength(0);
            _buffer.Write(remaining, 0, remaining.Length);
        }
        else
        {
            _buffer.SetLength(0);
        }
    }

    protected virtual void OnDataReceived(string obj)
    {
        DataReceived?.Invoke(obj);
    }
}
