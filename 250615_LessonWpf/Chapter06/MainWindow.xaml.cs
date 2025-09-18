using System.Windows;
using System.Windows.Input;

namespace Chapter06;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Btn0001_Click(object sender, RoutedEventArgs e) { }

    private void Executed_OnApplicationNeedNew(object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show($"来源是: {e.Source}");
    }

    private void Executed_OnCustomCommand(object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show($"自定义来源是: {e.Source}");
    }

    private void CusCommand_OnCanExecuteChanged(object? sender, EventArgs e)
    {
        MessageBox.Show("自定义状态改变");
    }

    private void TestActionFuncEx()
    {
        MessageBox.Show($"无参执行");
    }

    private void TestActionWithParameterFuncEx(string parameter)
    {
        MessageBox.Show($"有参执行: {parameter}");
    }

    private bool TestFunc()
    {
        MessageBox.Show($"返回值为false");
        return false;
    }

    private bool TestFuncWithParameter(string parameter)
    {
        return parameter == "牛牛牛";
    }
}
