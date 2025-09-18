using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows;

namespace MaApplications
{
    public partial class MainWindow
    {
        private enum PrintTypeOption
        {
            Info,
            Success,
            Warning,
            Error,
        }

        private SerialClient _serialClient;
        private string _oldInfo = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            InitCompoBoxInfo();
        }

        private void InitCompoBoxInfo()
        {
            PrintLog(PrintTypeOption.Info, "初始化");
            BtnRefreshPortOnClick(null!, null!);
            ComboBoxBaudRate.ItemsSource = new[]
            {
                1200,
                2400,
                4800,
                9600,
                14400,
                19200,
                38400,
                57600,
                115200,
            };
            ComboBoxBaudRate.SelectedIndex = 3;

            ComboBoxDataBits.ItemsSource = new[] { 5, 6, 7, 8 };
            ComboBoxDataBits.SelectedIndex = 3;

            ComboBoxStopBits.ItemsSource = SerialClient.GetStopBits();
            ComboBoxStopBits.SelectedIndex = 1;

            ComboBoxParity.ItemsSource = SerialClient.GetParity();
            ComboBoxParity.SelectedIndex = 0;

            PrintLog(PrintTypeOption.Info, "初始化完成");
        }

        private void PrintLog(PrintTypeOption printTypeOption, string message)
        {
            var time = DateTime.Now.ToString("HH:mm:ss");
            var type = $"[{printTypeOption}]";
            var log = $"{time}: {type} {message}\n";
            Dispatcher.Invoke(() =>
            {
                TextBoxLogs.AppendText(log);
                TextBoxLogs.ScrollToEnd();
            });
        }

        private void BtnRefreshPortOnClick(object sender, RoutedEventArgs e)
        {
            var portsName = SerialClient.GetPortsName();
            ComboBoxPortNames.ItemsSource = portsName;
            if (portsName.Length > 0)
            {
                PrintLog(PrintTypeOption.Info, $"找到{portsName.Length}个串口");
                ComboBoxPortNames.SelectedIndex = 0;
            }
            else
            {
                PrintLog(PrintTypeOption.Warning, "未找到任何串口");
            }
        }

        private void BtnCreatePortOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxPortNames.SelectionBoxItem == null)
                {
                    PrintLog(PrintTypeOption.Warning, "请选择串口");
                    return;
                }
                var name = ComboBoxPortNames.SelectionBoxItem.ToString();
                var baudRate = (int)ComboBoxBaudRate.SelectedItem;
                var dataBits = (int)ComboBoxDataBits.SelectedItem;
                var stopBits = (StopBits)ComboBoxStopBits.SelectedIndex;
                var parity = (Parity)ComboBoxParity.SelectedIndex;
                var serialPort = SerialClient.CreatePorts(
                    name!,
                    baudRate,
                    dataBits,
                    stopBits,
                    parity
                );
                _serialClient = new SerialClient(serialPort);
                PrintLog(PrintTypeOption.Success, "创建串口成功");
            }
            catch (Exception ex)
            {
                PrintLog(PrintTypeOption.Error, ex.Message);
            }
        }

        private void BtnOpenPortOnClick(object sender, RoutedEventArgs e)
        {
            if (_serialClient == null)
            {
                PrintLog(PrintTypeOption.Warning, "请先创建串口");
                return;
            }
            _serialClient.DataReceived += OnDataReceived;
            _serialClient.OpenClose(true);
            PrintLog(PrintTypeOption.Success, "打开串口成功");
        }

        private void BtnClosePortOnClick(object sender, RoutedEventArgs e)
        {
            if (_serialClient == null)
            {
                PrintLog(PrintTypeOption.Warning, "请先创建串口");
                return;
            }
            _serialClient.DataReceived -= OnDataReceived;
            _serialClient.OpenClose(false);
            PrintLog(PrintTypeOption.Success, "关闭串口成功");
        }

        private void OnDataReceived(string obj)
        {
            if (_oldInfo != obj)
            {
                _oldInfo = obj;
                var info = obj.Replace(".", " ");
                var matches = MyRegex().Matches(info);
                if (matches.Count == 3)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Label1.Content = $"温度 :{matches[0].Value}℃";
                        Label2.Content = $"功率 :{matches[1].Value} / 9";
                        Label3.Content = $"保温 :{matches[2].Value}℃";
                    });
                }
                PrintLog(PrintTypeOption.Info, $"数据更新: {obj}");
            }
        }

        [GeneratedRegex(@"\d+")]
        private static partial Regex MyRegex();
    }
}
