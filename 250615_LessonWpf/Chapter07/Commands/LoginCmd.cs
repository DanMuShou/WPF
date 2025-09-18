using System.Windows.Input;

namespace Chapter07.Commands;

/// <summary>
/// 命令 - 登录
/// </summary>
public class LoginCmd : ICommand
{
    /// <summary>
    /// 委托
    /// </summary>
    public required Func<bool> CanExecuteFunc { get; init; }

    /// <summary>
    /// 是否可执行
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    /// <summary>
    /// 执行
    /// </summary>
    public void Execute(object? parameter)
    {
        CanExecuteFunc?.Invoke();
    }

    public event EventHandler? CanExecuteChanged;
}
