using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ProductionMonitoringPlatform.Models;

internal enum MachineStatusEnum
{
    Working,
    Waiting,
    Wrong,
    Stop,
}

public partial class MachineModel : ObservableObject
{
    [ObservableProperty]
    private string _machineName = "Null";

    [ObservableProperty]
    private string _status = nameof(MachineStatusEnum.Stop);

    [ObservableProperty]
    private int _planCount;

    [ObservableProperty]
    private int _finishedCount;

    [ObservableProperty]
    private string _orderNo = "Null";

    public double Percent
    {
        get
        {
            if (PlanCount == 0)
                return 0;
            return Math.Round(FinishedCount * 100.0 / PlanCount, 2);
        }
    }

    public Brush StatusColor
    {
        get
        {
            return Status switch
            {
                nameof(MachineStatusEnum.Working) => Brushes.Green,
                nameof(MachineStatusEnum.Waiting) => Brushes.Yellow,
                nameof(MachineStatusEnum.Wrong) => Brushes.Red,
                nameof(MachineStatusEnum.Stop) => Brushes.Purple,
                _ => Brushes.Gray,
            };
        }
    }
}
