using System.Windows;
using System.Windows.Media.Animation;

namespace Chapter10;

public class MyEase : EasingFunctionBase
{
    /// <summary>
    /// 创建缓动实例
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override Freezable CreateInstanceCore()
    {
        return new MyEase();
    }

    /// <summary>
    /// 缓动方法
    /// </summary>
    /// <param name="normalizedTime"> 0 - 1</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override double EaseInCore(double normalizedTime)
    {
        return Math.Pow(normalizedTime, 2);
    }

    public static readonly DependencyProperty JitterProperty = DependencyProperty.Register(
        nameof(Jitter),
        typeof(int),
        typeof(MyEase),
        new PropertyMetadata(1000),
        new ValidateValueCallback(ValidateValueCallback)
    );

    /// <summary>
    ///   验证回调
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private static bool ValidateValueCallback(object value)
    {
        throw new NotImplementedException();
    }

    public int Jitter
    {
        get { return (int)GetValue(JitterProperty); }
        set { SetValue(JitterProperty, value); }
    }
}
