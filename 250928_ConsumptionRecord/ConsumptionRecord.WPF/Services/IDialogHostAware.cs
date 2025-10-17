namespace ConsumptionRecord.WPF.Services;

/// <summary>
/// 定义对话框宿主感知接口，用于处理对话框相关的操作和命令
/// </summary>
public interface IDialogHostAware
{
    /// <summary>
    /// 当对话框打开时触发的回调方法
    /// </summary>
    /// <param name="parameters">对话框参数，包含传递给对话框的数据</param>
    void OnDialogOpening(IDialogParameters parameters);

    /// <summary>
    /// 获取保存操作的委托命令
    /// </summary>
    DelegateCommand SaveCommand { get; }

    /// <summary>
    /// 获取取消操作的委托命令
    /// </summary>
    DelegateCommand CancelCommand { get; }
}
