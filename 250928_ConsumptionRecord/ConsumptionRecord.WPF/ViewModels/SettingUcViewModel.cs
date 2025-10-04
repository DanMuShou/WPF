using ConsumptionRecord.WPF.Models;
using ConsumptionRecord.WPF.Views;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.ViewModels;

public class SettingUcViewModel : BindableBase
{
    #region 属性

    private List<LeftMenuInfo> _leftMenuInfos;

    public List<LeftMenuInfo> LeftMenuInfos
    {
        get => _leftMenuInfos;
        set => SetProperty(ref _leftMenuInfos, value);
    }
    #endregion

    #region 命令
    public DelegateCommand<LeftMenuInfo> NavigateCommand { get; set; }

    #endregion

    private readonly IRegionManager _regionManager;
    private IRegionNavigationJournal _journal;

    public SettingUcViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        NavigateCommand = new DelegateCommand<LeftMenuInfo>(Navigate);

        CreateLeftMenu();
    }

    private void CreateLeftMenu()
    {
        LeftMenuInfos =
        [
            new LeftMenuInfo(PackIconKind.CogOutline, "设置", nameof(SystemSettingUc)),
            new LeftMenuInfo(PackIconKind.Palette, "个性化", nameof(PersonalUc)),
            new LeftMenuInfo(PackIconKind.InformationOutline, "关于", nameof(AboutUsUc)),
        ];
    }

    private void Navigate(LeftMenuInfo leftMenuInfo)
    {
        if (string.IsNullOrWhiteSpace(leftMenuInfo?.ViewName))
            return;

        const string regionName = "SettingUcRegion";
        if (_regionManager.Regions.ContainsRegionWithName(regionName))
        {
            _regionManager
                .Regions[regionName]
                .RequestNavigate(
                    leftMenuInfo.ViewName,
                    result => _journal = result.Context.NavigationService.Journal
                );
        }
    }
}
