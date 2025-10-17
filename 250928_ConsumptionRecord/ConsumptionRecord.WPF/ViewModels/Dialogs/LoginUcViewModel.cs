using System.Net.Http;
using ConsumptionRecord.WPF.Clients.ApiClient;
using ConsumptionRecord.WPF.Events;
using ConsumptionRecord.WPF.Models;

namespace ConsumptionRecord.WPF.ViewModels.Dialogs;

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

    private UserRegisteredModel _userRegisterModel = new();
    public UserRegisteredModel UserRegisterModel
    {
        get => _userRegisterModel;
        set => SetProperty(ref _userRegisterModel, value);
    }

    private UserLoginModel _userLoginModel = new();
    public UserLoginModel UserLoginModel
    {
        get => _userLoginModel;
        set => SetProperty(ref _userLoginModel, value);
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
        if (true)
        {
            var dialogResult = new DialogResult(ButtonResult.OK)
            {
                Parameters = new DialogParameters
                {
                    {
                        nameof(UserInfoDto),
                        new UserInfoDto() { ResponseTime = DateTime.Now, UserName = "admin" }
                    },
                },
            };
            RequestClose.Invoke(dialogResult);
        }
        else
        {
            // if (string.IsNullOrWhiteSpace(UserLoginModel.Account) || string.IsNullOrWhiteSpace(Pwd))
            // {
            //     _aggregator.GetEvent<MessageEvent>().Publish("请填写完整信息");
            //     return;
            // }
            // var userLoginDto = new UserLoginDto() { Account = UserLoginModel.Account, Password = Pwd };
            // try
            // {
            //     var apiClient = new ApiClient("https://localhost:7019", new HttpClient());
            //     var result = await apiClient.LoginAsync(userLoginDto);
            //     if (result is { Success: true })
            //     {
            //         _aggregator.GetEvent<MessageEvent>().Publish("登录成功");
            //         var dialogResult = new DialogResult(ButtonResult.OK)
            //         {
            //             Parameters = new DialogParameters
            //             {
            //                 { result.Data.GetType().Name, result.Data },
            //             },
            //         };
            //         RequestClose.Invoke(dialogResult);
            //     }
            // }
            // catch (Exception ex)
            // {
            //     _aggregator.GetEvent<MessageEvent>().Publish(ex.Message);
            // }
        }
    }

    private async void Registered()
    {
        if (
            string.IsNullOrWhiteSpace(UserRegisterModel.UserName)
            || string.IsNullOrWhiteSpace(UserRegisterModel.Account)
            || string.IsNullOrWhiteSpace(UserRegisterModel.Password)
            || string.IsNullOrWhiteSpace(UserRegisterModel.ConfirmPassword)
        )
        {
            _aggregator.GetEvent<MessageEvent>().Publish("请填写完整信息");
            return;
        }
        if (UserRegisterModel.Password != UserRegisterModel.ConfirmPassword)
        {
            _aggregator.GetEvent<MessageEvent>().Publish("密码不一致");
            return;
        }
        var userRegisterDto = new UserRegisterDto()
        {
            UserName = UserRegisterModel.UserName,
            Account = UserRegisterModel.Account,
            Password = UserRegisterModel.Password,
        };
        var apiClient = new ApiClient("https://localhost:7019", new HttpClient());
        try
        {
            var result = await apiClient.RegisteredAsync(userRegisterDto);
            if (result is { Success: true })
            {
                _aggregator.GetEvent<MessageEvent>().Publish("注册成功" + result.Data.ResponseTime);
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
