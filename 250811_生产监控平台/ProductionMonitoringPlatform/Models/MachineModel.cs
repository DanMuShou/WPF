using ProductionMonitoringPlatform.Models.Base;

namespace ProductionMonitoringPlatform.Models;

public class MachineModel : NotifyBaseModel
{
    private string _machineCount = "Null";

    public string MachineCount
    {
        get => _machineCount;
        set => SetField(ref _machineCount, value);
    }

    private string _productCount = "Null";
    public string ProductCount
    {
        get => _productCount;
        set => SetField(ref _productCount, value);
    }

    private string _failuresCount = "Null";
    public string FailuresCount
    {
        get => _failuresCount;
        set => SetField(ref _failuresCount, value);
    }
}
