using CommunityToolkit.Mvvm.ComponentModel;

namespace ProductionMonitoringPlatform.Models;

public partial class WorkShopModel : ObservableObject
{
    [ObservableProperty]
    private string _workShopName = "Null";

    [ObservableProperty]
    private int _workingCount = 0;

    [ObservableProperty]
    private int _waitCount = 0;

    [ObservableProperty]
    private int _stopCount = 0;

    [ObservableProperty]
    private int _wrongCount = 0;

    public int TotalCount => WorkingCount + WaitCount + StopCount + WrongCount;
}
