namespace Chapter07.Models;

/// <summary>
/// 数据模型 - 登录
/// </summary>
public class LoginModel
{
    // public event PropertyChangedEventHandler? PropertyChanged; // 属性改变事件
    //
    // protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    // {
    //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    // }
    //
    // protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    // {
    //     if (EqualityComparer<T>.Default.Equals(field, value))
    //         return false;
    //     field = value;
    //     OnPropertyChanged(propertyName);
    //     return true;
    // }

    private string _account = string.Empty;

    /// <summary>
    /// 账户
    /// </summary>
    public required string Account
    {
        get => _account;
        set => _account = value;
        // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Account)));
    }

    private string _password = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public required string Password
    {
        get => _password;
        set => _password = value;
        // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
    }
}
