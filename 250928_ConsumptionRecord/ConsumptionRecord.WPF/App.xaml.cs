using System.Net.Http;
using System.Windows;
using ConsumptionRecord.WPF.Clients.ApiClient;
using ConsumptionRecord.WPF.Services;
using ConsumptionRecord.WPF.ViewModels;
using ConsumptionRecord.WPF.ViewModels.Dialogs;
using ConsumptionRecord.WPF.Views;
using ConsumptionRecord.WPF.Views.Dialogs;

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
        containerRegistry.RegisterForNavigation<WaitUc, WaitUcViewModel>();
        containerRegistry.RegisterForNavigation<SettingUc, SettingUcViewModel>();

        containerRegistry.RegisterForNavigation<PersonalUc, PersonalUcViewModel>();
        containerRegistry.RegisterForNavigation<AboutUsUc, AboutUsUcViewModel>();
        containerRegistry.RegisterForNavigation<SystemSettingUc, SystemSettingUcViewModel>();

        containerRegistry.RegisterForNavigation<AddWaitUc, AddWaitUcViewModel>();

        containerRegistry.Register<DialogHostService>();

        containerRegistry.RegisterSingleton<ApiClient>(provider => new ApiClient(
            "https://localhost:7019",
            new HttpClient()
        ));
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
        // base.OnInitialized();

        var dialog = Container.Resolve<IDialogService>();
        dialog.ShowDialog(
            nameof(LoginUc),
            callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                if (Current.MainWindow.DataContext is MainWindowViewModel viewModel)
                {
                    var navigationParameters = new NavigationParameters();
                    foreach (var key in callback.Parameters.Keys)
                        navigationParameters.Add(key, callback.Parameters.GetValue<object>(key));

                    viewModel.SetDefaultNavigation(nameof(HomeUc), navigationParameters);
                    base.OnInitialized();
                }
            }
        );
    }
}
