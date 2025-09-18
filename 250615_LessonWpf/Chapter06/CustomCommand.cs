using System.Windows;
using System.Windows.Input;

namespace Chapter06;

/// <summary>
/// 自定义命令 把业务处理传递给方法(使用系统委托)
/// </summary>
public class CustomCommand : ICommand
{
    // 1. 没有返回值没有参数
    public Action MyAction { get; set; }

    // 2. 没有返回值有参数
    public Action<string> MyActionWithParameter { get; set; }

    // 3. 有返回值没有参数
    public Func<bool> MyFunc { get; set; }

    // 4. 有返回值有参数
    public Func<string, bool> MyFuncWithParameter { get; set; }

    /// <summary>
    /// 能否执行
    /// </summary>
    /// <param name="parameter">必须有参数才可以执行</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool CanExecute(object? parameter)
    {
        return parameter != null;
    }

    /// <summary>
    /// 如何执行
    /// </summary>
    /// <param name="parameter"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Execute(object? parameter)
    {
        MessageBox.Show($"命令执行, 参数为{parameter}");

        MyAction?.Invoke();
        MyActionWithParameter?.Invoke(parameter?.ToString()!);
        var result = MyFunc?.Invoke();
        MessageBox.Show($"返回值为{result}");
        var result2 = MyFuncWithParameter?.Invoke(parameter?.ToString()!);
        MessageBox.Show($"返回值为22{result2}");
    }

    /// <summary>
    /// 命令状态改变事件
    /// </summary>
    public event EventHandler? CanExecuteChanged;
}
