using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProductionMonitoringPlatform.Models;

namespace ProductionMonitoringPlatform.Controls;

public partial class RenderUc : UserControl
{
    public RenderUc()
    {
        InitializeComponent();

        SizeChanged += OnSizeChanged;
    }

    public static readonly DependencyProperty ItemSourceProperty =
        DependencyProperty.RegisterAttached(
            nameof(ItemSource),
            typeof(List<RenderModel>),
            typeof(RenderUc),
            new PropertyMetadata(default(List<RenderModel>))
        );

    public List<RenderModel> ItemSource
    {
        get => (List<RenderModel>)GetValue(ItemSourceProperty);
        set => SetValue(ItemSourceProperty, value);
    }

    private void Draw()
    {
        if (MainCanvas == null || ItemSource == null || ItemSource.Count == 0)
            return;

        MainCanvas.Children.Clear();
        // P1.Points.Clear();
        // P2.Points.Clear();
        // P3.Points.Clear();
        // P4.Points.Clear();
        // PDate.Points.Clear();

        var minSize = MathF.Min((float)RenderSize.Width, (float)RenderSize.Height);

        LayGrid.Height = minSize;
        LayGrid.Width = minSize;

        var radius = minSize / 2;
        var step = 360f / ItemSource.Count;

        P1.Points.Add(new Point(0, 0));
        P2.Points.Add(new Point(20, 0));
        P3.Points.Add(new Point(10, 10));
        P4.Points.Add(new Point(0, 10));

        for (var i = 0; i < ItemSource.Count; i++)
        {
            var x = (radius - 20) * MathF.Cos(step * i - 90 * MathF.PI / 180);
            var y = (radius - 20) * MathF.Sin(step * i - 90 * MathF.PI / 180);
            // P1.Points.Add(new Point(radius + x, radius + y));
            // P2.Points.Add(new Point(radius + x * 0.75, radius + y * 0.75));
            // P3.Points.Add(new Point(radius + x * 0.5, radius + y * 0.5));
            // P4.Points.Add(new Point(radius + x * 0.25, radius + y * 0.25));

            // if (!int.TryParse(ItemSource[i].ItemValue, out var number))
            //     continue;
            // PDate.Points.Add(new Point(radius + x * number * 0.01f, radius + y * number * 0.01f));
            //
            // var text = new TextBlock()
            // {
            //     Width = 60,
            //     FontSize = 10,
            //     Text = ItemSource[i].ItemName,
            //     HorizontalAlignment = HorizontalAlignment.Center,
            //     TextAlignment = TextAlignment.Center,
            //     Foreground = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255)),
            // };
            // text.SetValue(
            //     Canvas.LeftProperty,
            //     radius + (radius - 10) * MathF.Cos((step * i - 90) * MathF.PI / 180) - 30
            // );
            // text.SetValue(
            //     Canvas.TopProperty,
            //     radius + (radius - 10) * MathF.Sin((step * i - 90) * MathF.PI / 180) - 7
            // );
            // MainCanvas.Children.Add(text);
        }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        Draw();
    }
}
