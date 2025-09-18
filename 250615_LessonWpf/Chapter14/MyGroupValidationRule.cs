using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Chapter14;

public class MyGroupValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        if (value is BindingGroup group)
        {
            if (group.Items[0] is Product product)
            {
                var name = product.Name;
                var price = product.Price;
                var count = product.Count;

                if (price * count > 1000)
                {
                    return new ValidationResult(false, "总价不能超过1000");
                }
            }
        }
        return new ValidationResult(true, null);
    }
}
