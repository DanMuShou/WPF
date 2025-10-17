using System.Windows;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.Services;

public class DialogHostService(IContainerExtension containerExtension)
    : DialogService(containerExtension)
{
    public async Task<IDialogResult> ShowDialog(
        string dialogName,
        IDialogParameters? parameters,
        string hostName = "RootDialog"
    )
    {
        parameters ??= new DialogParameters();
        var content = containerExtension.Resolve<object>(dialogName);
        if (content is not FrameworkElement dialogContent)
            throw new NullReferenceException($"{dialogName} is not a FrameworkElement");

        // 检查 dialogContent 的 DataContext 是否为空且未自动绑定 ViewModel
        // 如果条件满足，则启用自动绑定 ViewModel 功
        if (
            dialogContent.DataContext is null
            && ViewModelLocator.GetAutoWireViewModel(dialogContent) is null
        )
            ViewModelLocator.SetAutoWireViewModel(dialogContent, true);

        if (dialogContent.DataContext is not IDialogHostAware hostViewModel)
            throw new NullReferenceException($"{dialogName} is not a IDialogHostAware");

        // 创建对话框打开事件处理器 Material Design 对话框打开时的回调操作
        var handler = new DialogOpenedEventHandler(
            (sender, eventArgs) =>
            {
                hostViewModel.OnDialogOpening(parameters);
                eventArgs.Session.UpdateContent(dialogContent);
            }
        );

        return (IDialogResult)await DialogHost.Show(dialogContent, hostName, handler);
    }
}
