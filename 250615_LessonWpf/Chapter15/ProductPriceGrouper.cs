using System.Globalization;
using System.Windows.Data;

namespace Chapter15;

public class ProductPriceGrouper : IValueConverter
{
    public decimal PriceInterval { get; set; } = 50.0m;

    /// <summary>
    /// 将值转换为目标类型
    /// </summary>
    /// <param name="value">要转换的值</param>
    /// <param name="targetType">目标类型</param>
    /// <param name="parameter">转换器参数</param>
    /// <param name="culture">区域性信息</param>
    /// <returns>转换后的值</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!decimal.TryParse(value?.ToString(), out var price))
            return "N/A";

        if (price < PriceInterval)
        {
            return $"Low than {PriceInterval:C}";
        }
        else
        {
            var interval = (int)(price / PriceInterval);
            var minVal = interval * PriceInterval;
            var maxVal = minVal + PriceInterval;
            return $"Range {minVal:C} - {maxVal:C}";
        }
    }

    /// <summary>
    /// 将值转换回源类型
    /// </summary>
    /// <param name="value">要转换的值</param>
    /// <param name="targetType">目标类型</param>
    /// <param name="parameter">转换器参数</param>
    /// <param name="culture">区域性信息</param>
    /// <returns>转换后的值</returns>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
