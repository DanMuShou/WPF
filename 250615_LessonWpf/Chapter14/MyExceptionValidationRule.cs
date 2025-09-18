using System.Globalization;
using System.Windows.Controls;

namespace Chapter14;

public class MyExceptionValidationRule : ValidationRule
{
    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        return value is decimal
            ? new ValidationResult(true, null)
            : new ValidationResult(false, "价格格式错误");
    }
}
