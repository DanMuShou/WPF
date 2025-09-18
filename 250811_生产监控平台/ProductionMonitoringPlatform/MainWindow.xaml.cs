using System.Windows;
using ProductionMonitoringPlatform.ViewModels;

namespace ProductionMonitoringPlatform;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    #region 窗口状态事件

    /// <summary>
    /// 处理最小化按钮点击事件，将窗口状态设置为最小化。
    /// </summary>
    /// <param name="sender">事件源对象</param>
    /// <param name="e">路由事件参数</param>
    /// <returns>无返回值</returns>
    private void BtnMinimize_OnClick(object sender, RoutedEventArgs e) =>
        WindowState = WindowState.Minimized;

    /// <summary>
    /// 处理最大化按钮点击事件，切换窗口的正常与最大化状态。
    /// 当窗口处于最大化状态时恢复为正常状态，否则进入最大化状态。
    /// </summary>
    /// <param name="sender">事件源对象</param>
    /// <param name="e">路由事件参数</param>
    /// <returns>无返回值</returns>
    private void BtnMaximize_OnCLick(object sender, RoutedEventArgs e) =>
        WindowState =
            WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

    /// <summary>
    /// 处理关闭按钮点击事件，关闭当前窗口。
    /// </summary>
    /// <param name="sender">事件源对象</param>
    /// <param name="e">路由事件参数</param>
    /// <returns>无返回值</returns>
    private void BtnCloseWindows_OnClick(object sender, RoutedEventArgs e) => Close();

    #endregion
}
