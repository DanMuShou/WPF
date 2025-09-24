using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductionMonitoringPlatform.Controls;
using ProductionMonitoringPlatform.Models;

namespace ProductionMonitoringPlatform.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl _monitorUc = new MonitorUc();

    #region 公共属性
    public TimeStrModel TimeStrModel { get; } = new();
    public CapacityModel CapacityModel { get; } = new();

    public List<EnvironmentModel> EnvironmentModelList { get; } =
    [
        new() { EnItemName = "光照(Lux)", EnItemValue = "null" },
        new() { EnItemName = "温度(℃)", EnItemValue = "null" },
        new() { EnItemName = "噪音(db)", EnItemValue = "null" },
        new() { EnItemName = "湿度(%RH)", EnItemValue = "null" },
        new() { EnItemName = "CO2(ppm)", EnItemValue = "null" },
        new() { EnItemName = "PM2.5(ug/m3)", EnItemValue = "null" },
        new() { EnItemName = "VOC(ppm)", EnItemValue = "null" },
    ];

    public List<AlarmModel> AlarmModel { get; } =
    [
        new()
        {
            Numbering = "01",
            Message = "设备1异常",
            Time = "2023-05-01 10:00",
            Duration = "1H",
        },
        new()
        {
            Numbering = "02",
            Message = "设备2异常",
            Time = "2023-05-01 10:00",
            Duration = "1H",
        },
        new()
        {
            Numbering = "03",
            Message = "设备3异常",
            Time = "2023-05-01 10:00",
            Duration = "1H",
        },
        new()
        {
            Numbering = "04",
            Message = "设备4异常",
            Time = "2023-05-01 10:00",
            Duration = "1H",
        },
    ];

    public List<DeviceModel> DeviceModelList { get; } =
    [
        new() { DeviceName = "电能(Kw.h)", DeviceValue = "null" },
        new() { DeviceName = "电压(V)", DeviceValue = "null" },
        new() { DeviceName = "电流(A)", DeviceValue = "null" },
        new() { DeviceName = "压差(kpa)", DeviceValue = "null" },
        new() { DeviceName = "温度(°C)", DeviceValue = "null" },
        new() { DeviceName = "震动(mm/s)", DeviceValue = "null" },
        new() { DeviceName = "转速(r/min)", DeviceValue = "null" },
        new() { DeviceName = "气压(kpa)", DeviceValue = "null" },
    ];

    public List<RenderModel> RenderModelList { get; } =
    [
        new() { ItemName = "设备1", ItemValue = "100" },
        new() { ItemName = "设备2", ItemValue = "200" },
        new() { ItemName = "设备3", ItemValue = "100" },
        new() { ItemName = "设备4", ItemValue = "300" },
        new() { ItemName = "设备5", ItemValue = "500" },
    ];

    public List<StuffOutWorkModel> StuffOutWorkModelList { get; } =
    [
        new()
        {
            StuffName = "Name1",
            Position = "Pos1",
            OutWorkCount = 1.ToString(),
        },
        new()
        {
            StuffName = "Name2",
            Position = "Pos2",
            OutWorkCount = 2.ToString(),
        },
        new()
        {
            StuffName = "Name3",
            Position = "Pos3",
            OutWorkCount = 3.ToString(),
        },
        new()
        {
            StuffName = "Name4",
            Position = "Pos4",
            OutWorkCount = 40.ToString(),
        },
        new()
        {
            StuffName = "Name5",
            Position = "Pos5",
            OutWorkCount = 35.ToString(),
        },
    ];

    public List<WorkShopModel> WorkShopLists { get; } =
    [
        new()
        {
            WorkShopName = "Null1",
            WorkingCount = 1,
            WaitCount = 2,
            WrongCount = 3,
            StopCount = 4,
        },
        new()
        {
            WorkShopName = "Null2",
            WorkingCount = 0,
            WaitCount = 0,
            WrongCount = 0,
            StopCount = 0,
        },
        new()
        {
            WorkShopName = "Null3",
            WorkingCount = 0,
            WaitCount = 0,
            WrongCount = 0,
            StopCount = 0,
        },
        new()
        {
            WorkShopName = "Null4",
            WorkingCount = 0,
            WaitCount = 0,
            WrongCount = 0,
            StopCount = 0,
        },
    ];

    public List<MachineModel> MachineModelList { get; } = [];

    #endregion

    public MainWindowViewModel()
    {
        var radom = new Random();
        var status = Enum.GetValues(typeof(MachineStatusEnum)).Cast<MachineStatusEnum>().ToList();
        for (var i = 0; i < 20; i++)
        {
            var plant = radom.Next(100, 1000);
            var finished = radom.Next(0, plant);
            var index = radom.Next(status.Count);
            MachineModelList.Add(
                new MachineModel()
                {
                    MachineName = $"Null {i + 1}",
                    PlanCount = plant,
                    FinishedCount = finished,
                    Status = status[index].ToString(),
                    OrderNo = "100Null",
                }
            );
        }
    }

    #region 命令

    [RelayCommand]
    private void ShowDetailUc()
    {
        var workShopDetailUc = new WorkShopDetailUC();
        MonitorUc = workShopDetailUc;

        var animationTime = new Duration(TimeSpan.FromMilliseconds(200));

        // 位移动画
        var thicknessAnimation = new ThicknessAnimation(
            new Thickness(0, 50, 0, -50),
            new Thickness(0, 0, 0, 0),
            animationTime
        );
        // 透明度动画
        var opacityAnimation = new DoubleAnimation(0, 1, animationTime);

        // 设置动画目标
        Storyboard.SetTarget(thicknessAnimation, workShopDetailUc);
        Storyboard.SetTarget(opacityAnimation, workShopDetailUc);

        // 设置动画属性
        Storyboard.SetTargetProperty(
            thicknessAnimation,
            new PropertyPath(nameof(workShopDetailUc.Margin))
        );
        Storyboard.SetTargetProperty(
            opacityAnimation,
            new PropertyPath(nameof(workShopDetailUc.Opacity))
        );

        var storyboard = new Storyboard();
        storyboard.Children.Add(thicknessAnimation);
        storyboard.Children.Add(opacityAnimation);
        storyboard.Begin();
    }

    [RelayCommand]
    private void GoBack()
    {
        MonitorUc = new MonitorUc();
    }

    #endregion
}
