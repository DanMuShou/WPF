using System.Windows.Controls;
using ConsumptionRecord.WPF.Events;

namespace ConsumptionRecord.WPF.Views.Dialogs;

/// <summary>
/// 登录用户控件类，负责处理登录界面的显示和消息订阅功能
/// </summary>
public partial class LoginUc : Page
{
    /// <summary>
    /// 初始化登录用户控件实例
    /// </summary>
    /// <param name="aggregator">事件聚合器，用于订阅和发布消息事件</param>
    public LoginUc(IEventAggregator aggregator)
    {
        InitializeComponent();
        var aggregator1 = aggregator;
        // 订阅消息事件，当有消息发布时会调用SubMessageQueue方法处理
        aggregator1.GetEvent<MessageEvent>().Subscribe(SubMessageQueue);
    }

    /// <summary>
    /// 处理接收到的消息，将消息添加到消息队列中
    /// </summary>
    /// <param name="message">接收到的消息内容</param>
    private void SubMessageQueue(string message) => MessageBar.MessageQueue.Enqueue(message);
}
