using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProductionMonitoringPlatform.Controls;

public partial class RingUc : UserControl
{
    public RingUc()
    {
        InitializeComponent();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        Drug();
    }

    public static readonly DependencyProperty PercentValueProperty = DependencyProperty.Register(
        nameof(PercentValue),
        typeof(double),
        typeof(RingUc),
        new PropertyMetadata(0.0)
    );

    public double PercentValue
    {
        get => (double)GetValue(PercentValueProperty);
        set => SetValue(PercentValueProperty, value);
    }

    private void Drug()
    {
        MainGrid.Width = Math.Min(RenderSize.Width, RenderSize.Height);
        var radius = MainGrid.Width / 2;

        var x = radius + (radius - 3) * Math.Cos((PercentValue % 100 * 3.6 - 90) * Math.PI / 180);
        var y = radius + (radius - 3) * Math.Sin((PercentValue % 100 * 3.6 - 90) * Math.PI / 180);

        var is50 = PercentValue < 50 ? 0 : 1;
        var pathStr = $"M{radius + 0.01} 3A{radius - 3} {radius - 3} 0 {is50} 1 {x} {y}";

        var converter = TypeDescriptor.GetConverter(typeof(Geometry));
        RingPath.Data = (Geometry)converter.ConvertFrom(pathStr);
    }
}
