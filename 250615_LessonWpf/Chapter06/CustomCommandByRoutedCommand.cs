using System.Windows.Input;

namespace Chapter06;

/// <summary>
/// 通过 RoutedCommand 实现自定义命令
/// </summary>
public class CustomCommandByRoutedCommand
{
    private static readonly RoutedUICommand _query;

    static CustomCommandByRoutedCommand()
    {
        // 应提供命令名称、命令的文本（通常用于界面显示）、拥有该命令的类型
        _query = new RoutedUICommand("Query", "Query Data", typeof(CustomCommandByRoutedCommand));
    }

    public static RoutedCommand Query => _query;
}
