using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chapter13.MUserControl;

public partial class MColorPicker : UserControl
{
    public MColorPicker()
    {
        InitializeComponent();
        SetUpCommands();
    }

    private Color? _perviousColor;

    #region 颜色 RGB 单通道

    /// <summary>
    /// 依赖属性，用于表示颜色的红色分量值
    /// </summary>
    public static readonly DependencyProperty ColorRProperty = DependencyProperty.Register(
        nameof(ColorR),
        typeof(int),
        typeof(MColorPicker),
        new FrameworkPropertyMetadata(
            127,
            FrameworkPropertyMetadataOptions.AffectsRender,
            ColorChannelPropertyChangedCallback,
            CoerceValueCallback
        ),
        ValidateValueCallback
    );

    /// <summary>
    /// 获取或设置颜色的红色分量值，取值范围为0-255
    /// </summary>
    public int ColorR
    {
        get => (int)GetValue(ColorRProperty);
        set => SetValue(ColorRProperty, value);
    }

    /// <summary>
    /// 依赖属性，用于表示颜色的绿色分量值
    /// </summary>
    public static readonly DependencyProperty ColorGProperty = DependencyProperty.Register(
        nameof(ColorG),
        typeof(int),
        typeof(MColorPicker),
        new FrameworkPropertyMetadata(
            127,
            FrameworkPropertyMetadataOptions.AffectsRender,
            ColorChannelPropertyChangedCallback,
            CoerceValueCallback
        ),
        ValidateValueCallback
    );

    /// <summary>
    /// 获取或设置颜色的绿色分量值，取值范围为0-255
    /// </summary>
    public int ColorG
    {
        get => (int)GetValue(ColorGProperty);
        set => SetValue(ColorGProperty, value);
    }

    /// <summary>
    /// 依赖属性，用于表示颜色的蓝色分量值
    /// </summary>
    public static readonly DependencyProperty ColorBProperty = DependencyProperty.Register(
        nameof(ColorB),
        typeof(int),
        typeof(MColorPicker),
        new FrameworkPropertyMetadata(
            127,
            FrameworkPropertyMetadataOptions.AffectsRender,
            ColorChannelPropertyChangedCallback,
            CoerceValueCallback
        ),
        ValidateValueCallback
    );

    /// <summary>
    /// 获取或设置颜色的蓝色分量值，取值范围为0-255
    /// </summary>
    public int ColorB
    {
        get => (int)GetValue(ColorBProperty);
        set => SetValue(ColorBProperty, value);
    }

    /// <summary>
    /// 强制值回调函数，确保值在 0-255 范围内
    /// </summary>
    /// <param name="d">依赖对象</param>
    /// <param name="baseValue">基础值</param>
    /// <returns>调整后的值</returns>
    private static object CoerceValueCallback(DependencyObject d, object baseValue)
    {
        if (int.TryParse(baseValue.ToString(), out var num))
        {
            return num < 0 ? 0
                : num > 255 ? 255
                : num;
        }
        return 127;
    }

    /// <summary>
    /// 验证值回调函数，检查值是否在有效范围内
    /// </summary>
    /// <param name="value">待验证的值</param>
    /// <returns>验证结果，true表示有效，false表示无效</returns>
    private static bool ValidateValueCallback(object value)
    {
        if (int.TryParse(value.ToString(), out var num))
        {
            return num is >= 0 and <= 255;
        }
        return false;
    }

    private static void ColorChannelPropertyChangedCallback(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (d is MColorPicker colorPicker && e.NewValue is int num)
        {
            var propertyName = e.Property.Name;
            colorPicker.Color = propertyName switch
            {
                nameof(ColorR) => Color.FromArgb(
                    255,
                    (byte)num,
                    colorPicker.Color.G,
                    colorPicker.Color.B
                ),
                nameof(ColorG) => Color.FromArgb(
                    255,
                    colorPicker.Color.R,
                    (byte)num,
                    colorPicker.Color.B
                ),
                nameof(ColorB) => Color.FromArgb(
                    255,
                    colorPicker.Color.R,
                    colorPicker.Color.G,
                    (byte)num
                ),
                _ => colorPicker.Color,
            };
        }
    }

    #endregion

    #region 颜色

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(Color),
        typeof(MColorPicker),
        new PropertyMetadata(Colors.White, ColorPropertyChangedCallback)
    );

    private static void ColorPropertyChangedCallback(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (d is MColorPicker colorPicker && e.NewValue is Color color)
        {
            colorPicker.ColorR = color.R;
            colorPicker.ColorG = color.G;
            colorPicker.ColorB = color.B;

            var oldColor = (Color)e.OldValue;
            var args = new RoutedPropertyChangedEventArgs<Color>(oldColor, color)
            {
                RoutedEvent = colorPicker._colorChangedEvent, // 指定事件源
            };
            colorPicker.RaiseEvent(args); // 触发 ColorChanged 事件

            colorPicker._perviousColor = oldColor;
        }
    }

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    #endregion

    #region 公开事件

    private readonly RoutedEvent _colorChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(ColorChanged),
        RoutingStrategy.Bubble,
        typeof(RoutedPropertyChangedEventHandler<Color>),
        typeof(MColorPicker)
    );

    public event RoutedPropertyChangedEventHandler<Color> ColorChanged
    {
        add => AddHandler(_colorChangedEvent, value);
        remove => RemoveHandler(_colorChangedEvent, value);
    }

    #endregion

    private void SetUpCommands()
    {
        var binding = new CommandBinding(ApplicationCommands.Undo, Executed, CanExecute);
        CommandBindings.Add(binding);
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Color = _perviousColor!.Value;
        _perviousColor = null;
    }

    /// <summary>
    /// 判断命令是否可用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
        e.CanExecute = _perviousColor.HasValue;
}
