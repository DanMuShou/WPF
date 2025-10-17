using System.Windows;
using ConsumptionRecord.WPF.Clients.ApiClient;
using ConsumptionRecord.WPF.Models;
using ConsumptionRecord.WPF.Services;
using ConsumptionRecord.WPF.Views;
using ConsumptionRecord.WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.ViewModels;

public class HomeUcViewModel : BindableBase, INavigationAware
{
    #region 属性

    private DateTime _cloudTime;

    public DateTime CloudTime
    {
        get => _cloudTime;
        set => SetProperty(ref _cloudTime, value);
    }

    private string _loginName;

    public string LoginName
    {
        get => _loginName;
        set => SetProperty(ref _loginName, value);
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

    #endregion 属性

    #region 命令

    public DelegateCommand ShowAddWaitDialogCommand { get; }
    public DelegateCommand<WaitInfo> ChangeWaitInfoCommand { get; }
    public DelegateCommand<WaitInfo> ShowEditWaitDialogCommand { get; }
    public DelegateCommand<StatePanelInfo> NavigateDetailCommand { get; }
    #endregion 命令

    private readonly DialogHostService _dialogHostService;
    private readonly ApiClient _apiClient;
    private readonly IRegionManager _regionManager;

    public HomeUcViewModel(
        DialogHostService dialogHostService,
        ApiClient apiClient,
        IRegionManager regionManager
    )
    {
        _dialogHostService = dialogHostService;
        _apiClient = apiClient;
        _regionManager = regionManager;

        ShowAddWaitDialogCommand = new DelegateCommand(ShowAddWaitDialog);
        ShowEditWaitDialogCommand = new DelegateCommand<WaitInfo>(ShowEditWaitDialog);
        ChangeWaitInfoCommand = new DelegateCommand<WaitInfo>(ChangeWaitInfo);
        NavigateDetailCommand = new DelegateCommand<StatePanelInfo>(NavigateDetail);

        CreateStatePanel();
        CreateMemo();

        GetAllWaits();
    }

    #region Api

    private async Task GetWaitStat()
    {
        var apiClient = new ApiClient("https://localhost:7019", new System.Net.Http.HttpClient());

        try
        {
            var response = await apiClient.GetStatAsync();
            if (response.Success)
            {
                StatePanelInfos[0].Result = response.Data.TotalCount.ToString();
                StatePanelInfos[1].Result = response.Data.FinishCount.ToString();
                StatePanelInfos[2].Result = (
                    response.Data.TotalCount > 0
                        ? response.Data.FinishCount / (double)response.Data.TotalCount
                        : 0
                ).ToString("P");
            }
            else
            {
                MessageBox.Show(string.Join(", ", response.Errors));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async Task PostWaitInfo(WaitInfo waitInfo)
    {
        try
        {
            var waitInfoDto = new WaitInfoDto()
            {
                Title = waitInfo.Title,
                Content = waitInfo.Content,
                IsDone = waitInfo.IsDone,
            };
            var response = await _apiClient.AddWaitAsync(waitInfoDto);
            if (response.Success)
            {
                await GetWaitStat();
                await GetAllWaits();
            }
            else
                MessageBox.Show(string.Join(", ", response.Errors));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async Task GetAllWaits()
    {
        try
        {
            var result = await _apiClient.GetWaitsAsync();
            if (result.Success)
            {
                WaitInfos = result
                    .Data.Select(x => new WaitInfo()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Content = x.Content,
                        IsDone = x.IsDone,
                        Time = x.CreateTime.DateTime,
                    })
                    .OrderByDescending(x => x.Time)
                    .ToList();
            }
            else
            {
                MessageBox.Show(string.Join(", ", result.Errors));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async Task PutWait(WaitInfo waitInfo)
    {
        try
        {
            var waitInfoDto = new WaitInfoDto()
            {
                Id = waitInfo.Id,
                Title = waitInfo.Title,
                Content = waitInfo.Content,
                IsDone = waitInfo.IsDone,
                CreateTime = waitInfo.Time,
            };
            var response = await _apiClient.UpdateWaitStatusAsync(waitInfoDto);
            if (response.Success)
            {
                await GetWaitStat();
                await GetAllWaits();
            }
            else
                MessageBox.Show(string.Join(", ", response.Errors));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion Api

    private async void ShowAddWaitDialog()
    {
        var result = await _dialogHostService.ShowDialog(nameof(AddWaitUc), null);
        if (result.Result == ButtonResult.OK)
        {
            if (result.Parameters.TryGetValue<WaitInfo>(nameof(WaitInfo), out var waitInfo))
            {
                await PostWaitInfo(waitInfo);
            }
        }
    }

    private async void ShowEditWaitDialog(WaitInfo selectWaitInfo)
    {
        var parameter = new DialogParameters { { nameof(WaitInfo), selectWaitInfo } };
        var result = await _dialogHostService.ShowDialog(nameof(AddWaitUc), parameter);
        if (result.Result == ButtonResult.OK)
        {
            if (result.Parameters.TryGetValue<WaitInfo>(nameof(WaitInfo), out var waitInfo))
            {
                await PutWait(waitInfo);
            }
        }
    }

    private async void ChangeWaitInfo(WaitInfo selectWaitInfo)
    {
        await PutWait(selectWaitInfo);
    }

    private void NavigateDetail(StatePanelInfo selectStatePanelInfo)
    {
        const string regionName = "MainViewRegion";
        if (string.IsNullOrWhiteSpace(selectStatePanelInfo.ViewType?.Name))
            return;

        if (_regionManager.Regions.ContainsRegionWithName(regionName))
        {
            _regionManager
                .Regions[regionName]
                .RequestNavigate(
                    selectStatePanelInfo.ViewType.Name,
                    selectStatePanelInfo.Parameters
                );
        }
    }

    #region Init

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
                ViewType = typeof(WaitUc),
                Parameters = new NavigationParameters() { { "status", 0 } },
            },
            new StatePanelInfo()
            {
                Title = "已完成",
                Result = string.Empty,
                BackColor = "#825BCD",
                Icon = PackIconKind.BookCheckOutline,
                ViewType = typeof(WaitUc),
                Parameters = new NavigationParameters() { { "status", 2 } },
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
                ViewType = typeof(MemoUc),
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

    #endregion Init

    #region IConfirmNavigationRequest

    public async void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (
            navigationContext.Parameters.TryGetValue<UserInfoDto>(
                nameof(UserInfoDto),
                out var userInfoDto
            )
        )
        {
            CloudTime = userInfoDto.ResponseTime.LocalDateTime;
            LoginName = userInfoDto.UserName;
        }

        await GetWaitStat();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedFrom(NavigationContext navigationContext) { }

    #endregion IConfirmNavigationRequest
}
