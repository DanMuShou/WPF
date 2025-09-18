using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class AlarmModel : NotifyBaseModel
{
    private string _numbering;

    /// <summary>
    /// 获取或设置编号属性
    /// </summary>
    public string Numbering
    {
        get => _numbering;
        set => SetField(ref _numbering, value);
    }

    private string _message;

    /// <summary>
    /// 获取或设置消息内容属性
    /// </summary>
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    private string _time;

    /// <summary>
    /// 获取或设置时间属性
    /// </summary>
    public string Time
    {
        get => _time;
        set => SetField(ref _time, value);
    }

    private string _duration;

    /// <summary>
    /// 获取或设置持续时间属性
    /// </summary>
    public string Duration
    {
        get => _duration;
        set => SetField(ref _duration, value);
    }
}
