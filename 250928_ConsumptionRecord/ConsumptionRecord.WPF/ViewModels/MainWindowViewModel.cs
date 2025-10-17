using ConsumptionRecord.WPF.Models;
using ConsumptionRecord.WPF.Views;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.ViewModels;

public class MainWindowViewModel : BindableBase
{
    #region 属性
    private List<LeftMenuInfo> _leftMenuInfos =
    [
        new(PackIconKind.Home, "首页", nameof(HomeUc)),
        new(PackIconKind.CurrencyUsd, "消费记录", nameof(WaitUc)),
        new(PackIconKind.NotebookPlus, "备忘录", nameof(MemoUc)),
        new(PackIconKind.Cog, "设置", nameof(SettingUc)),
    ];

    public List<LeftMenuInfo> LeftMenuInfos
    {
        get => _leftMenuInfos;
        set => SetProperty(ref _leftMenuInfos, value);
    }
    #endregion

    #region Command
    public DelegateCommand<LeftMenuInfo> NavigateCommand { get; set; }
    public DelegateCommand GoForwardCommand { get; set; }
    public DelegateCommand GoBackCommand { get; set; }
    #endregion

    private readonly IRegionManager _regionManager;
    private IRegionNavigationJournal _journal;

    public MainWindowViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        NavigateCommand = new DelegateCommand<LeftMenuInfo>(Navigate);
        GoForwardCommand = new DelegateCommand(GoNext);
        GoBackCommand = new DelegateCommand(GoBack);
    }

    public void SetDefaultNavigation(string ucName, INavigationParameters? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(ucName))
            return;

        const string regionName = "MainViewRegion";
        if (_regionManager.Regions.ContainsRegionWithName(regionName))
        {
            _regionManager
                .Regions[regionName]
                .RequestNavigate(
                    ucName,
                    result => _journal = result.Context.NavigationService.Journal,
                    parameters
                );
        }
    }

    private void Navigate(LeftMenuInfo info)
    {
        if (string.IsNullOrWhiteSpace(info.ViewName))
            return;

        const string regionName = "MainViewRegion";
        if (_regionManager.Regions.ContainsRegionWithName(regionName))
        {
            _regionManager
                .Regions[regionName]
                .RequestNavigate(
                    info.ViewName,
                    result => _journal = result.Context.NavigationService.Journal
                );
        }
    }

    private void GoNext()
    {
        if (_journal is { CanGoForward: true })
            _journal.GoForward();
    }

    private void GoBack()
    {
        if (_journal is { CanGoBack: true })
            _journal.GoBack();
    }
}
