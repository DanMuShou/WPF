using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProductionMonitoringPlatform.Controls;
using ProductionMonitoringPlatform.Models;

namespace ProductionMonitoringPlatform.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        _monitorUc = new MonitorUc();
    }

    private MonitorUc _monitorUc;

    /// <summary>
    /// 获取或设置监控用户控件实例
    /// </summary>
    /// <remarks>
    /// 当值发生更改时会触发PropertyChanged事件通知
    /// </remarks>
    public MonitorUc MonitorUc
    {
        get => _monitorUc;
        set => SetField(ref _monitorUc, value);
    }

    public TimeStrModel TimeStrModel { get; } = new();
    public MachineModel MachineModel { get; } = new();

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

    #region 通知接口实现
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    #endregion
}
