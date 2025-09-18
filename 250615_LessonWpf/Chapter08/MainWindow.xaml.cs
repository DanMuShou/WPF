using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chapter08;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Btn01_Click(object sender, RoutedEventArgs e)
    {
        Style newStyle = new Style(typeof(Button));
        newStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Red));
        Resources["BtnStyle"] = newStyle;
    }

    private void EventSetter_OnHandler(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("显示");
    }
}
