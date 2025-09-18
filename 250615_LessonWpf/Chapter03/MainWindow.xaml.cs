using System.Windows;
using System.Windows.Input;

namespace Chapter03;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Border_UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("Border");
        e.Handled = true; // 阻止事件继续传播 =>
    }

    private void StackPanel_UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("StackPanel");
    }

    private void WindowsMouseButPress(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("Windows");
    }

    private void BtnMouseLeftBtn(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("Button");
    }

    private void borderPreDumn(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("MainWindow.xaml.cs");
    }

    private void borderPrsseDumn(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("MainWindow.xaml.cs2");
    }

    private bool _isShowMousePos = false;

    private void GridMouseMove(object sender, MouseEventArgs e)
    {
        if (!_isShowMousePos)
        {
            Info.Text = string.Empty;
            return;
        }

        var pos = e.GetPosition(Grid);
        Info.Text = $"X:{pos.X} Y:{pos.Y}";
    }

    private void GirdMouseDown(object sender, MouseButtonEventArgs e)
    {
        // 获取鼠标在任何地方都能被捕获
        Mouse.Capture((UIElement)sender);
        // ((UIElement)sender).CaptureMouse();  相同

        _isShowMousePos = true;
    }

    private void GridMouseUp(object sender, MouseButtonEventArgs e)
    {
        // 释放捕获
        Mouse.Capture(null);
        // ((UIElement)sender).ReleaseMouseCapture();   相同
        _isShowMousePos = false;
    }
}
