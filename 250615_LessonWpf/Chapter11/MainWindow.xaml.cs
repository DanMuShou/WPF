using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chapter11;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private bool _rendering;
    private List<EllipseInfo> _ellipseInfos = [];

    private readonly Brush _ellipseBrush = Brushes.Red;
    private const int EllipseRadius = 10;
    private const int MinEllipse = 20;
    private const int MaxEllipse = 100;

    private void Btn_OnClickStart(object sender, RoutedEventArgs e)
    {
        if (!_rendering)
        {
            _ellipseInfos.Clear();
            Canvas1101.Children.Clear();

            CompositionTarget.Rendering += RenderingFrame;
        }
    }

    private void RenderingFrame(object? sender, EventArgs e)
    {
        if (_ellipseInfos.Count == 0)
        {
            var random = new Random();
            var randomCount = random.Next(MinEllipse, MaxEllipse + 1);

            var canvasW = (int)Canvas1101.ActualWidth;
            var canvasH = (int)Canvas1101.ActualHeight / 3;
            for (var i = 0; i < randomCount; i++)
            {
                var ellipse = new Ellipse
                {
                    Fill = _ellipseBrush,
                    Width = EllipseRadius,
                    Height = EllipseRadius,
                };

                Canvas.SetLeft(ellipse, random.Next(0, canvasW));
                Canvas.SetTop(ellipse, random.Next(0, canvasH));
                Canvas1101.Children.Add(ellipse);

                var velocityY = random.Next(1, 5);

                _ellipseInfos.Add(new EllipseInfo(ellipse, velocityY));
            }
        }
        else
        {
            var fallH = Canvas1101.ActualHeight - EllipseRadius * 2;
            for (var i = 0; i < _ellipseInfos.Count; i++)
            {
                var info = _ellipseInfos[i];
                var top = Canvas.GetTop(info.Ellipse);

                Canvas.SetTop(info.Ellipse, top + info.VelocityY);

                if (top > fallH)
                {
                    _ellipseInfos.Remove(info);
                }
                else
                {
                    info.VelocityY += 0.1;
                }

                if (_ellipseInfos.Count == 0)
                {
                    StopRendering();
                }
            }
        }
    }

    private void Btn_OnClickStop(object sender, RoutedEventArgs e)
    {
        StopRendering();
    }

    private void StopRendering()
    {
        CompositionTarget.Rendering -= RenderingFrame;
        _rendering = false;
    }
}
