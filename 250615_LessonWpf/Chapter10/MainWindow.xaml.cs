using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chapter10;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button011_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        button.Width = 10;
        var animation = new DoubleAnimation
        {
            From = button.ActualWidth,
            To = button.ActualWidth * 2,
            Duration = TimeSpan.FromSeconds(1),
        };
        button.BeginAnimation(WidthProperty, animation);
    }

    private void Btn0201_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        var animation = new DoubleAnimation()
        {
            From = button.ActualWidth,
            To = button.ActualWidth * 2,
            // By = 20,
            Duration = TimeSpan.FromSeconds(1),
        };
        animation.Completed += (o, args) =>
        {
            button.BeginAnimation(WidthProperty, null);
            MessageBox.Show("动画结束");
        };
        button.BeginAnimation(WidthProperty, animation);
    }

    private void Btn0202_Click(object sender, RoutedEventArgs e)
    {
        Btn0201.Width = 10;
        var end = Btn0201.Width;
    }

    private void Btn0203_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        var animation = new DoubleAnimation()
        {
            From = button.ActualWidth,
            To = button.ActualWidth * 2,
            BeginTime = TimeSpan.FromSeconds(5),
            Duration = TimeSpan.FromSeconds(1),
            SpeedRatio = 2, // 播放速度倍率
            // AutoReverse = true, // 播放完后反播
            FillBehavior = FillBehavior.Stop, // 完成操作返回最早的数值
            RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(13)), // 在一定时间内反播
            AccelerationRatio = 0.3, // 速度由快到慢 前30% 快到慢
        };
        button.BeginAnimation(WidthProperty, animation);
    }

    /// <summary>
    /// 监视播放进度
    /// </summary>
    /// <param name="sender">故事板</param>
    /// <param name="e"></param>
    private void TimeLine_OnCurrentTimeInvalidated(object? sender, EventArgs e)
    {
        if (sender is not Clock clock)
            return;

        var process = clock.CurrentProgress;
        if (!process.HasValue)
        {
            ProgressBar.Value = 0;
            LabPlayInfo.Content = "播放结束";
        }
        else
        {
            ProgressBar.Value = process.Value;
            LabPlayInfo.Content = $"播放进度：{process.Value * 100:F0}%";
        }
    }

    /// <summary>
    /// 播放速度进度条改变重新设置播放速度
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is Slider slider)
        {
            Storyboard.SetSpeedRatio(slider.Value);
        }
    }
}
