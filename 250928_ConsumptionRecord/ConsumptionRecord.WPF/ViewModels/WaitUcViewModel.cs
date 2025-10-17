using System.Windows;
using ConsumptionRecord.WPF.Clients.ApiClient;
using ConsumptionRecord.WPF.Models;

namespace ConsumptionRecord.WPF.ViewModels;

public class WaitUcViewModel : BindableBase, INavigationAware
{
    #region 属性
    private List<WaitInfo> _waitInfos;
    public List<WaitInfo> WaitInfos
    {
        get => _waitInfos;
        set => SetProperty(ref _waitInfos, value);
    }

    private bool _isShowAddWait;
    public bool IsShowAddWait
    {
        get => _isShowAddWait;
        set => SetProperty(ref _isShowAddWait, value);
    }

    private int _selectModeIndex;
    public int SelectModeIndex
    {
        get => _selectModeIndex;
        set => SetProperty(ref _selectModeIndex, value);
    }

    private string _selectQueryStr;
    public string SelectQueryStr
    {
        get => _selectQueryStr;
        set => SetProperty(ref _selectQueryStr, value);
    }
    #endregion

    #region 命令
    public DelegateCommand ShowAddWaitCommand { get; }
    public DelegateCommand QueryWaitListCommand { get; }
    #endregion

    private readonly ApiClient _apiClient;

    public WaitUcViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;

        ShowAddWaitCommand = new DelegateCommand(ShowAddWait);
        QueryWaitListCommand = new DelegateCommand(QueryWaitList);
    }

    #region API

    private List<WaitInfo> GetWaitList(string? queryStr, int status = 0)
    {
        try
        {
            return _apiClient
                .GetWaitsWithFilterAsync(queryStr, status)
                .Result.Data.Select(x => new WaitInfo
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Time = x.CreateTime.DateTime,
                    IsDone = x.IsDone,
                })
                .ToList();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return [];
        }
    }

    #endregion

    private void QueryWaitList()
    {
        WaitInfos = GetWaitList(SelectQueryStr, SelectModeIndex);
    }

    private void CreateWait()
    {
        _waitInfos = [];
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const int length = 5;
        var random = new Random();
        for (var i = 0; i < 50; i++)
        {
            _waitInfos.Add(
                new WaitInfo
                {
                    Title = new string(
                        Enumerable
                            .Repeat(chars, length)
                            .Select(s => s[random.Next(s.Length)])
                            .ToArray()
                    ),
                    Content = new string(
                        Enumerable
                            .Repeat(chars, length)
                            .Select(s => s[random.Next(s.Length)])
                            .ToArray()
                    ),
                    Time = DateTime.Now.AddDays(i),
                }
            );
        }
    }

    private void ShowAddWait() => IsShowAddWait = true;

    #region INavigationAware

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.TryGetValue<int>("status", out var status))
        {
            SelectModeIndex = status;
            WaitInfos = GetWaitList(SelectQueryStr, status);
            return;
        }

        WaitInfos = GetWaitList(SelectQueryStr);
    }

    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedFrom(NavigationContext navigationContext) { }

    #endregion
}
