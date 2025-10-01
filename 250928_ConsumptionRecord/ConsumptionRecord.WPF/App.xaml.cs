using System.Windows;
using ConsumptionRecord.WPF.ViewModels;
using ConsumptionRecord.WPF.Views;

namespace ConsumptionRecord.WPF;

/// <summary>
/// 应用程序主类，继承自PrismApplication
/// </summary>
public partial class App : PrismApplication
{
    /// <summary>
    /// 注册应用程序所需的服务和类型到依赖注入容器
    /// </summary>
    /// <param name="containerRegistry">容器注册器，用于注册服务和类型</param>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterDialog<LoginUc, LoginUcViewModel>();

        containerRegistry.RegisterForNavigation<MemoUc, MemoUcViewModel>();
        containerRegistry.RegisterForNavigation<HomeUc, HomeUcViewModel>();
        containerRegistry.RegisterForNavigation<ConsumptionUc, ConsumptionUcViewModel>();
        containerRegistry.RegisterForNavigation<SettingUc, SettingUcViewModel>();
    }

    /// <summary>
    /// 创建应用程序的主窗体实例
    /// </summary>
    /// <returns>返回主窗口实例</returns>
    protected override Window CreateShell()
    {
        // 返回主窗口实例
        return Container.Resolve<MainWindow>();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        // TODO : 登录
        // var dialog = Container.Resolve<IDialogService>();
        // dialog.ShowDialog(
        //     nameof(LoginUc),
        //     callback =>
        //     {
        //         if (callback.Result != ButtonResult.OK)
        //         {
        //             Environment.Exit(0);
        //             return;
        //         }
        //         base.OnInitialized();
        //     }
        // );
    }
}
