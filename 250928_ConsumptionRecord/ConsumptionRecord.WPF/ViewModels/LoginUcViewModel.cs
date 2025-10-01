using System.Net.Http;
using System.Windows;
using ConsumptionRecord.WPF.Clients.ApiClient;
using ConsumptionRecord.WPF.Events;
using ConsumptionRecord.WPF.Models;

namespace ConsumptionRecord.WPF.ViewModels;

public class LoginUcViewModel : BindableBase, IDialogAware
{
    public string Title => "登录";

    #region 命令
    public DelegateCommand LoginCommand { get; }
    public DelegateCommand RegisteredCommand { get; }
    public DelegateCommand<string> ShowLoginPanelCommand { get; }
    #endregion

    #region 属性
    private int _selectedIndex;
    public int SelectedIndex
    {
        get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
    }

    private string _pwd = string.Empty;
    public string Pwd
    {
        get => _pwd;
        set => SetProperty(ref _pwd, value);
    }

    private UserLoginInfo _userRegisteredInfo = new();
    public UserLoginInfo UserRegisteredInfo
    {
        get => _userRegisteredInfo;
        set => SetProperty(ref _userRegisteredInfo, value);
    }

    private UserLoginInfo _userLoginInfo = new();
    public UserLoginInfo UserLoginInfo
    {
        get => _userLoginInfo;
        set => SetProperty(ref _userLoginInfo, value);
    }
    #endregion

    private IEventAggregator _aggregator;

    public LoginUcViewModel(IEventAggregator aggregator)
    {
        LoginCommand = new DelegateCommand(Login);
        RegisteredCommand = new DelegateCommand(Registered);
        ShowLoginPanelCommand = new DelegateCommand<string>(ShowLoginPanel);
        _aggregator = aggregator;
    }

    private async void Login()
    {
        if (string.IsNullOrWhiteSpace(UserLoginInfo.Email) || string.IsNullOrWhiteSpace(Pwd))
        {
            _aggregator.GetEvent<MessageEvent>().Publish("请填写完整信息");
            return;
        }
        try
        {
            var apiClient = new ApiClient("https://localhost:7019", new HttpClient());
            var result = await apiClient.LoginAsync(
                new UserInfoDto()
                {
                    UserName = "1",
                    Email = UserLoginInfo.Email,
                    Password = Pwd,
                }
            );
            if (result is { Success: true })
            {
                _aggregator.GetEvent<MessageEvent>().Publish("登录成功");
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            }
        }
        catch (ApiException ex)
        {
            _aggregator.GetEvent<MessageEvent>().Publish(ex.Message);
        }
    }

    private async void Registered()
    {
        if (
            string.IsNullOrWhiteSpace(UserRegisteredInfo.UserName)
            || string.IsNullOrWhiteSpace(UserRegisteredInfo.Email)
            || string.IsNullOrWhiteSpace(UserRegisteredInfo.Password)
            || string.IsNullOrWhiteSpace(UserRegisteredInfo.ConfirmPassword)
        )
        {
            _aggregator.GetEvent<MessageEvent>().Publish("请填写完整信息");
            return;
        }
        if (UserRegisteredInfo.Password != UserRegisteredInfo.ConfirmPassword)
        {
            _aggregator.GetEvent<MessageEvent>().Publish("密码不一致");
            return;
        }
        var userInfoDto = new UserInfoDto()
        {
            UserName = UserRegisteredInfo.UserName,
            Email = UserRegisteredInfo.Email,
            Password = UserRegisteredInfo.Password,
            IsAdmin = false,
        };
        var apiClient = new ApiClient("https://localhost:7019", new HttpClient());
        try
        {
            var result = await apiClient.RegisteredAsync(userInfoDto);
            if (result is { Success: true })
            {
                _aggregator.GetEvent<MessageEvent>().Publish("注册成功");
                SelectedIndex = 0;
            }
            if (result is { Success: false })
                _aggregator.GetEvent<MessageEvent>().Publish(string.Join(", ", result.Errors));
        }
        catch (ApiException ex)
        {
            _aggregator.GetEvent<MessageEvent>().Publish(ex.Message);
        }
    }

    private void ShowLoginPanel(string index)
    {
        SelectedIndex = int.TryParse(index, out var num) ? num : 0;
    }

    #region IDialogAware
    public DialogCloseListener RequestClose { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters) { }

    #endregion
}
