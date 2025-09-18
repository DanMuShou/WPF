using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class DeviceModel : NotifyBaseModel
{
    private string _deviceName;
    public string DeviceName
    {
        get => _deviceName;
        set => SetField(ref _deviceName, value);
    }

    private string _deviceValue;
    public string DeviceValue
    {
        get => _deviceValue;
        set => SetField(ref _deviceValue, value);
    }
}
