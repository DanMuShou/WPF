using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ConsumptionRecord.WPF.Converter;

public class ColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isDone)
        {
            return isDone ? Brushes.DodgerBlue : Brushes.MediumSeaGreen;
        }
        return Brushes.Red;
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
