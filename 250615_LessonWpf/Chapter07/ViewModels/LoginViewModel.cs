using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Chapter07.Commands;
using Chapter07.Models;

namespace Chapter07.ViewModels;

/// <summary>
/// 视图模型 - 登录
/// </summary>
public class LoginViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) //这里使用 EqualityComparer<T>.Default.Equals 来比较当前字段的值和新值是否相等。
            return false;
        field = value;
        OnPropertyChanged(propertyName); //调用 OnPropertyChanged 方法，并传入属性名称。这个方法通常用于触发 PropertyChanged 事件，通知 UI 属性已经更改
        return true;
    }

    private LoginModel _loginModel = new() { Account = string.Empty, Password = string.Empty };

    // /// <summary>
    // /// 登录模型
    // /// </summary>
    // public LoginModel LoginModel
    // {
    //     get => _loginModel;
    //     set => SetField(ref _loginModel, value);
    // }

    private string _account = string.Empty;

    /// <summary>
    /// 账号
    /// </summary>
    public string Account
    {
        get => _account;
        set => SetField(ref _account, value);
    }
    private string _password = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    public LoginCmd LoginCmd
    {
        get
        {
            var loginCmd = new LoginCmd { CanExecuteFunc = Login };
            return loginCmd;
        }
    }

    /// <summary>
    /// 登录逻辑
    /// </summary>
    /// <returns>是否登录成功</returns>
    private bool Login()
    {
        var account = Account;
        var password = Password;
        if (account == "admin" && password == "123")
        {
            MessageBox.Show("登录成功");
            return true;
        }

        MessageBox.Show("账号或密码错误");
        Account = string.Empty;
        Password = string.Empty;
        return false;
    }
}
