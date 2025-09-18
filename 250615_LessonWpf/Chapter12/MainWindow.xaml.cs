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
using System.Windows.Threading;

namespace Chapter12;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool _isStart;
    private int _handleCount;
    private int _unhandleCount;

    private double _initRefreshBoomTime = 1.5;
    private double _initBombFallTime = 3.0;
    private double _nextRefreshBoomTime;
    private double _nextBombFallTime;

    private DateTime _lastGameDifficultyAdjustTime = DateTime.Now;
    private double _gameDifficultyAdjustInterval = 15;

    private DispatcherTimer _timer = new(); // UI线程触发
    private Dictionary<Bome, Storyboard> _dictionary = new(); // 保存炸弹动画

    public MainWindow()
    {
        InitializeComponent();

        MainCanvas.SizeChanged += MainCanvasOnSizeChanged;
        _timer.Tick += OnBombTimerTick;
    }

    private void But_OnGameStart(object sender, RoutedEventArgs e)
    {
        BtnStartGame.IsEnabled = false;

        _handleCount = 0;
        _unhandleCount = 0;
        _nextRefreshBoomTime = _initRefreshBoomTime;
        _nextBombFallTime = _initBombFallTime;

        TextScores.Text = $"分数 : {_handleCount}";
        TextTimeInfo.Text = $"时间 : {_nextRefreshBoomTime}";

        _timer.Interval = TimeSpan.FromSeconds(_nextRefreshBoomTime);
        _timer.Start();
    }

    private void OnBombTimerTick(object? sender, EventArgs e)
    {
        if (
            DateTime.Now.Subtract(_lastGameDifficultyAdjustTime).TotalSeconds
            > _gameDifficultyAdjustInterval
        ) // 游戏难度调整
        {
            _nextRefreshBoomTime -= 0.1;
            _nextBombFallTime -= 0.1;
            TextFallTime.Text = $"掉落时间 : {_nextRefreshBoomTime:F2}";
            TextNextBomb.Text = $"刷新时间 : {_nextBombFallTime:F2}";
            _lastGameDifficultyAdjustTime = DateTime.Now;

            _timer.Interval = TimeSpan.FromSeconds(_nextRefreshBoomTime);
        }

        var bomb = new Bome();

        var random = new Random();
        var left = random.Next(0, (int)MainCanvas.ActualWidth - 40);

        Canvas.SetTop(bomb, 0);
        Canvas.SetLeft(bomb, left);

        var storyboard = new Storyboard();
        var fallAnimation = new DoubleAnimation()
        {
            From = 0,
            To = MainCanvas.ActualHeight / 2,
            Duration = TimeSpan.FromSeconds(_nextBombFallTime),
            FillBehavior = FillBehavior.Stop,
        };
        Storyboard.SetTarget(fallAnimation, bomb);
        Storyboard.SetTargetProperty(fallAnimation, new PropertyPath(Canvas.TopProperty));

        bomb.MouseLeftButtonDown += BombOnMouseLeftButtonDown;
        _dictionary.Add(bomb, storyboard);

        storyboard.Children.Add(fallAnimation);
        MainCanvas.Children.Add(bomb);

        bomb.IsFalling = true;
        storyboard.Begin();
    }

    private void BombOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Bome bomb)
        {
            bomb.IsFalling = false;
            var storyboard = _dictionary[bomb];
            storyboard.Stop();
            var currentTop = Canvas.GetTop(bomb);
            Canvas.SetTop(bomb, currentTop);
            storyboard.Children.Clear();

            var currentL = Canvas.GetLeft(bomb);
            double shouldMovePos;
            if (currentL < MainCanvas.ActualWidth / 2)
            {
                shouldMovePos = 0;
            }
            else
            {
                shouldMovePos = MainCanvas.ActualWidth - 60;
            }
            var animation = new DoubleAnimation()
            {
                To = shouldMovePos,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            Storyboard.SetTarget(animation, bomb);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }

    /// <summary>
    /// 大小改变进行裁切, 防止图片超出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void MainCanvasOnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var rect = new RectangleGeometry
        {
            Rect = new Rect(0, 0, MainCanvas.ActualWidth, MainCanvas.ActualHeight),
        };
        MainCanvas.Clip = rect;
    }
}
