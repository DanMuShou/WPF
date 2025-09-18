using System.Windows;
using System.Windows.Controls;

namespace Chapter02;

internal class DependencyAttributes : Panel
{
    // 定义/注册/包装

    // 1. 定义
    //public static DependencyProperty AgeProperty { get; set; }

    // 2. 注册
    //static DependencyAttributes()
    //{
    //    AgeProperty = DependencyProperty.Register(
    //        "Age",
    //        typeof(int), // 数据类型
    //        typeof(DependencyAttributes) // 类型属于的类型
    //    );
    //}

    // 3. 包装
    //public int Age
    //{
    //    get { return (int)GetValue(AgeProperty); }
    //    set { SetValue(AgeProperty, value); }
    //}

    public DependencyAttributes()
    {
        Age = 1;
        Age = 200;
    }

    public int Age
    {
        get { return (int)GetValue(AgeProperty); }
        set { SetValue(AgeProperty, value); }
    }

    public static readonly DependencyProperty AgeProperty = DependencyProperty.Register(
        "Age",
        typeof(int),
        typeof(DependencyAttributes),
        new PropertyMetadata(0, OnAgeChange, OnCoerceAgeChange), // 默认值
        new ValidateValueCallback(OnValidAge)
    );

    // 属性值发生改变
    private static void OnAgeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // 执行业务

        // 获取值
        var oldValue = e.OldValue;
        var newValue = e.NewValue;
    }

    // 验证回调
    private static bool OnValidAge(object value) => true;

    // 强制验证回调
    private static object OnCoerceAgeChange(DependencyObject d, object baseValue)
    {
        var age = (int)baseValue;
        if (age >= 100)
        {
            return 100;
        }
        else
        {
            return age;
        }
    }

    // 依赖属性继承 被继承的属性
    public string StuName
    {
        get => (string)GetValue(StuNameProperty);
        set => SetValue(StuNameProperty, value);
    }

    // Using a DependencyProperty as the backing store for StuName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StuNameProperty = DependencyProperty.Register(
        nameof(StuName),
        typeof(string),
        typeof(DependencyAttributes),
        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOption s.Inherits)
    );
}
