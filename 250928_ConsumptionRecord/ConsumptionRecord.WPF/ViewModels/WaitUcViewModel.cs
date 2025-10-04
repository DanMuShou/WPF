using ConsumptionRecord.WPF.Models;

namespace ConsumptionRecord.WPF.ViewModels;

public class WaitUcViewModel : BindableBase
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
    #endregion

    #region 命令
    public DelegateCommand ShowAddWaitCommand { get; set; }
    #endregion


    public WaitUcViewModel()
    {
        ShowAddWaitCommand = new DelegateCommand(ShowAddWait);
        CreateWait();
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
}
