using System.Globalization;
using System.Windows.Data;

namespace ConsumptionRecord.WPF.Converter;

public enum ChineseType
{
    YMD,
    YMDM,
    YMDMC,
}

public class TimeToChineseConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateTime dateTime)
            return "转化错误";

        var week = dateTime.DayOfWeek switch
        {
            DayOfWeek.Sunday => "星期日",
            DayOfWeek.Monday => "星期一",
            DayOfWeek.Tuesday => "星期二",
            DayOfWeek.Wednesday => "星期三",
            DayOfWeek.Thursday => "星期四",
            DayOfWeek.Friday => "星期五",
            DayOfWeek.Saturday => "星期六",
            _ => "星期未知",
        };
        string time;
        if (Enum.TryParse<ChineseType>(parameter?.ToString(), out var type))
        {
            switch (type)
            {
                case ChineseType.YMD:
                    time = $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}";
                    break;
                case ChineseType.YMDM:
                    time = $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}";
                    time += $"{dateTime.DayOfWeek.ToString()}";
                    break;
                case ChineseType.YMDMC:
                    time = $"{dateTime.Year}年{dateTime.Month}月{dateTime.Day}日";
                    time += $" {week}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            time = $"{dateTime.Year}年{dateTime.Month}月{dateTime.Day}日";
            time += $" {week}";
        }

        return time;
    }

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
