using System.Windows.Threading;
using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class TimeStrModel : NotifyBaseModel
{
    public TimeStrModel()
    {
        _timeStr = DateTime.Now.ToString("HH:mm:ss");
        _dateStr = DateTime.Now.ToString("yyyy-MM-dd");
        _weekStr = DateTime.Now.ToString("dddd");

        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private readonly DispatcherTimer _timer;

    private string _timeStr;

    /// <summary>
    /// 获取或设置时间字符串属性
    /// </summary>
    /// <remarks>
    /// 当值发生变化时会触发PropertyChanged事件通知
    /// </remarks>
    public string TimeStr
    {
        get => _timeStr;
        private set => SetField(ref _timeStr, value);
    }

    private string _dateStr;

    /// <summary>
    /// 获取或设置日期字符串属性
    /// </summary>
    /// <remarks>
    /// 当值发生变化时会触发PropertyChanged事件通知
    /// </remarks>
    public string DateStr
    {
        get => _dateStr;
        private set => SetField(ref _dateStr, value);
    }

    private string _weekStr;

    /// <summary>
    /// 获取或设置星期字符串属性
    /// </summary>
    /// <remarks>
    /// 当值发生变化时会触发PropertyChanged事件通知
    /// </remarks>
    public string WeekStr
    {
        get => _weekStr;
        private set => SetField(ref _weekStr, value);
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        TimeStr = DateTime.Now.ToString("HH:mm:ss");
        DateStr = DateTime.Now.ToString("yyyy-MM-dd");
        WeekStr = DateTime.Now.ToString("dddd");
    }
}
