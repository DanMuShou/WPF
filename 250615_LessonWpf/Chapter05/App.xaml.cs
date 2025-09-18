using System.Windows;
using System.Windows.Threading;

namespace Chapter05;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        DispatcherUnhandledException += OnDispatcherUnhandledException;
    }

    /// <summary>
    /// 处理Ui异常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDispatcherUnhandledException(
        object sender,
        DispatcherUnhandledExceptionEventArgs e
    )
    {
        Console.WriteLine(e.Exception);
    }

    /// <summary>
    /// 应用启动
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {
        Console.WriteLine("OnStartUp");
        base.OnStartup(e);
    }

    /// <summary>
    /// 应用退出
    /// </summary>
    /// <param name="e"></param>
    protected override void OnExit(ExitEventArgs e)
    {
        Console.WriteLine("OnExit");
        base.OnExit(e);
    }

    /// <summary>
    /// 应用激活
    /// </summary>
    /// <param name="e"></param>
    protected override void OnActivated(EventArgs e)
    {
        Console.WriteLine("OnActivated");
        base.OnActivated(e);
    }

    /// <summary>
    /// 应用取消激活
    /// </summary>
    /// <param name="e"></param>
    protected override void OnDeactivated(EventArgs e)
    {
        Console.WriteLine("OnDeactivated");
        base.OnDeactivated(e);
    }

    /// <summary>
    /// 会话结束 关机时
    /// </summary>
    /// <param name="e"></param>
    protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
    {
        e.Cancel = true;
        base.OnSessionEnding(e);
    }
}
