using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chapter15;

public class PriceConvert : IValueConverter
{
    /// <summary>
    /// 获取或设置最小价格阈值
    /// </summary>
    public int MinPrice { get; set; }

    /// <summary>
    /// 获取或设置高价格时使用的画刷
    /// </summary>
    public Brush HighBrush { get; set; }

    /// <summary>
    /// 获取或设置低价格时使用的画刷
    /// </summary>
    public Brush LowBrush { get; set; }

    /// <summary>
    /// 根据价格值转换为对应的画刷
    /// </summary>
    /// <param name="value">要转换的价格值</param>
    /// <param name="targetType">目标类型</param>
    /// <param name="parameter">转换参数</param>
    /// <param name="culture">区域性信息</param>
    /// <returns>如果价格小于最小价格阈值则返回LowBrush，否则返回HighBrush；如果转换失败则返回null</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // 尝试将输入值解析为decimal类型的价格
        if (decimal.TryParse(value?.ToString(), out var price))
        {
            // 根据价格与最小价格阈值的比较结果返回相应的画刷
            return price < MinPrice ? LowBrush : HighBrush;
        }
        return null;
    }

    /// <summary>
    /// 反向转换方法（未实现）
    /// </summary>
    /// <param name="value">要转换的值</param>
    /// <param name="targetType">目标类型</param>
    /// <param name="parameter">转换参数</param>
    /// <param name="culture">区域性信息</param>
    /// <returns>始终返回null</returns>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return null;
    }
}
