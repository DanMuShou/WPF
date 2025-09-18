using System.Windows;
using System.Windows.Media.Imaging;

namespace Chapter05;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent(); // 逻辑树加载完成

        Loaded += OnLoaded; // 视觉树加载完成

        var xiaoMing = new Student() { Name = "小明", Age = 18 };
        //绑定到windows
        StackPanel2101.DataContext = xiaoMing;
    }

    private void Btn0001OnClick(object sender, RoutedEventArgs e)
    {
        var win1 = new Window1();
        win1.Show();
    }

    private void Btn0002Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Btn0003Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Btn0004Click(object sender, RoutedEventArgs e)
    {
        var thread = new Thread(() =>
        {
            Thread.Sleep(5000);
        });
        thread.Start();
    }

    private void Btn0005Click(object sender, RoutedEventArgs e)
    {
        GC.Collect();
        Environment.Exit(0);
    }

    /// <summary>
    /// 视觉树加载完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Show Image
        // Image0101.Source = new BitmapImage(new Uri("绝对路径"));

        // 相对路径
        // Image0101.Source = new BitmapImage(new Uri("Image/精灵-0001.png", UriKind.Relative));

        Image0101.Source = new BitmapImage(
            new Uri("pack://application:,,,/Chapter05;component/Image/精灵-0001.png")
        );
    }

    private void Btn0301OnClick(object sender, RoutedEventArgs e)
    {
        // Resource01.Source = new Uri("/Resources/MyLangageen.xaml", UriKind.Relative);
    }

    private void Btn1001OnClick(object sender, RoutedEventArgs e)
    {
        var radom = new Random();
        Image1001.Opacity = radom.NextDouble();
    }

    private void Btn10011OnClick(object sender, RoutedEventArgs e)
    {
        var radom = new Random();
        Slider1001.Value = radom.NextDouble();
    }
}

class Student
{
    public string? Name { get; set; }
    public int Age { get; set; }
}
