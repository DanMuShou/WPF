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

    private List<WaitInfo> _waitInfos;
    public List<WaitInfo> WaitInfos
    {
        get => _waitInfos;
        set => SetProperty(ref _waitInfos, value);
    }

    private List<MemoInfo> _memoInfos;
    public List<MemoInfo> MemoInfos
    {
        get => _memoInfos;
        set => SetProperty(ref _memoInfos, value);
    }
    #endregion

    public HomeUcViewModel()
    {
        CurrentTime = DateTime.Now;
        CreateStatePanel();
        CreateWait();
        CreateMemo();
    }

    private void CreateStatePanel()
    {
        _statePanelInfos =
        [
            new StatePanelInfo()
            {
                Title = "汇总",
                Result = string.Empty,
                BackColor = "#BC504A",
                Icon = PackIconKind.BookEditOutline,
                ViewType = null,
            },
            new StatePanelInfo()
            {
                Title = "已完成",
                Result = string.Empty,
                BackColor = "#825BCD",
                Icon = PackIconKind.BookCheckOutline,
                ViewType = null,
            },
            new StatePanelInfo()
            {
                Title = "完成比例",
                Result = string.Empty,
                BackColor = "#00789B",
                Icon = PackIconKind.ChartPieOutline,
                ViewType = null,
            },
            new StatePanelInfo()
            {
                Title = "备忘录",
                Result = string.Empty,
                BackColor = "#457E58",
                Icon = PackIconKind.NoteAlertOutline,
                ViewType = null,
            },
        ];
    }

    private void CreateWait()
    {
        _waitInfos =
        [
            new WaitInfo
            {
                Title = "测试一",
                Content = "这是测试一",
                Time = DateTime.Now,
            },
            new WaitInfo
            {
                Title = "测试二",
                Content = "这是测试二",
                Time = DateTime.Now,
            },
        ];
    }

    private void CreateMemo()
    {
        _memoInfos =
        [
            new MemoInfo
            {
                Title = "会议一",
                Content = "这是会议一",
                Time = DateTime.Now,
            },
            new MemoInfo
            {
                Title = "会议二",
                Content = "这是会议二",
                Time = DateTime.Now,
            },
        ];
    }
}
