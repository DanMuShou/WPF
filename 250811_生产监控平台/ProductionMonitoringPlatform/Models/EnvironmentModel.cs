using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class EnvironmentModel : NotifyBaseModel
{
    private string _enItemName = "Null";
    public string EnItemName
    {
        get => _enItemName;
        set => SetField(ref _enItemName, value);
    }
    private string _enItemValue = "Null";
    public string EnItemValue
    {
        get => _enItemValue;
        set => SetField(ref _enItemValue, value);
    }
}
