using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Chapter16;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// MainWindow构造函数，初始化组件并设置媒体播放相关功能
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _mediaIsPlaying = false;

        // 创建一个计时器，用于定期更新进度条
        var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += TimerOnTick;
        timer.Start();
    }

    private bool _mediaIsPlaying;

    /// <summary>
    /// 媒体元素鼠标左键抬起事件处理程序，用于切换播放和暂停状态
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">鼠标事件参数</param>
    private void Media_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_mediaIsPlaying)
        {
            Media.Pause();
        }
        else
        {
            Media.Play();
        }
        _mediaIsPlaying = !_mediaIsPlaying;
    }

    /// <summary>
    /// 打开按钮点击事件处理程序，用于选择并播放媒体文件
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">路由事件参数</param>
    private void BtnOpen_OnClick(object sender, RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog { Filter = "视频文件|*.mp4;*.avi;*.wmv;*.mkv" };
        if (fileDialog.ShowDialog() != true)
            return;

        Media.Source = new Uri(fileDialog.FileName);
        Media.MediaOpened += MediaOnMediaOpened;
        Media.Play();
        _mediaIsPlaying = true;
    }

    /// <summary>
    /// 暂停按钮点击事件处理程序，用于暂停媒体播放
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">路由事件参数</param>
    private void BtnPause_OnClick(object sender, RoutedEventArgs e)
    {
        if (!_mediaIsPlaying)
            return;
        Media.Pause();
        _mediaIsPlaying = false;
    }

    /// <summary>
    /// 媒体打开事件处理程序，设置进度条的最大值
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">路由事件参数</param>
    private void MediaOnMediaOpened(object sender, RoutedEventArgs e) =>
        ProBarMedia.Maximum = Media.NaturalDuration.TimeSpan.Seconds;

    /// <summary>
    /// 计时器触发事件处理程序，用于更新进度条的当前值
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">事件参数</param>
    private void TimerOnTick(object? sender, EventArgs e) =>
        ProBarMedia.Value = Media.Position.Seconds;

    private void BtnFastPlay_OnClick(object sender, RoutedEventArgs e)
    {
        Media.SpeedRatio = 2;
    }
}
