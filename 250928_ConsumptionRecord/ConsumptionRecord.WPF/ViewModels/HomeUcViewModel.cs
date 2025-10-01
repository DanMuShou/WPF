using System.Windows.Media;
using ConsumptionRecord.WPF.Models;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.ViewModels;

public class HomeUcViewModel : BindableBase
{
    #region 属性
    private DateTime _currentTime;
    public DateTime CurrentTime
    {
        get => _currentTime;
        set => SetProperty(ref _currentTime, value);
    }

    private List<StatePanelInfo> _statePanelInfos;
    public List<StatePanelInfo> StatePanelInfos
    {
        get => _statePanelInfos;
        set => SetProperty(ref _statePanelInfos, value);
    }
    #endregion

    public HomeUcViewModel()
    {
        CurrentTime = DateTime.Now;
        StatePanelInfos = new List<StatePanelInfo>()
        {
            new()
            {
                Title = "余额",
                Result = "0,00",
                Icon = PackIconKind.Cash100,
                BackColor = "#BC504A",
            },
            new()
            {
                Title = "花费",
                Result = "0.00",
                Icon = PackIconKind.CurrencyUsdOff,
                BackColor = "#825BCD",
            },
            new()
            {
                Title = "存入",
                Result = "0.00",
                Icon = PackIconKind.CurrencyUsd,
                BackColor = "#00789B",
            },
            new()
            {
                Title = "记录",
                Result = "0",
                Icon = PackIconKind.CalendarTextOutline,
                BackColor = "#457E58",
            },
        };
    }
}
