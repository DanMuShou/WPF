using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class StuffOutWorkModel : NotifyBaseModel
{
    private string _stuffName = "NULL";
    public string StuffName
    {
        get => _stuffName;
        set => SetField(ref _stuffName, value);
    }

    private string _position = "NULL";
    public string Position
    {
        get => _position;
        set => SetField(ref _position, value);
    }

    private int _outWorkCount = 0;
    public string OutWorkCount
    {
        get => _outWorkCount.ToString();
        set => SetField(ref _outWorkCount, int.Parse(value));
    }

    public int ShowWidth => _outWorkCount * 50 / 100;
}
