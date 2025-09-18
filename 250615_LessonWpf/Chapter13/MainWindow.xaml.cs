using System.Windows;
using System.Windows.Media;

namespace Chapter13;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MColorPicker01_OnColorChanged(
        object sender,
        RoutedPropertyChangedEventArgs<Color> e
    )
    {
        if (sender is MColorPicker colorPicker)
        {
            var color = colorPicker.Color.ToString();
            if (TextBlock01 != null)
            {
                TextBlock01.Text =
                    $"color: {color} R: {colorPicker.ColorR} G: {colorPicker.ColorG} B: {colorPicker.ColorB}";
            }
        }
    }
}
