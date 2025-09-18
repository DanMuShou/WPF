using System.Windows;
using System.Windows.Controls;

namespace Chapter04;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += ProcessBarLoaded;

        var countries = new List<Country>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "India",
                Code = "IN",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "United States",
                Code = "US",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "United Kingdom",
                Code = "UK",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Australia",
                Code = "AU",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Canada",
                Code = "CA",
            },
        };

        //设置数据源
        ListBox1101.ItemsSource = countries;
        ComboBox1301.ItemsSource = countries;
    }

    private void ProcessBarLoaded(object sender, RoutedEventArgs e)
    {
        var max = 40;
        ProgressBar2101.Minimum = 0;
        ProgressBar2101.Maximum = max;

        Task.Run(async () =>
        {
            for (var i = 0; i <= max; i++)
            {
                // Ui调用 Dispatcher来调用Ui
                var iUi = i;
                Dispatcher.Invoke(() =>
                {
                    ProgressBar2101.Value = iUi;
                    Label2101.Content = ((float)iUi / max).ToString("p");
                    if (iUi == max)
                    {
                        ProgressBar2101.Visibility = Visibility.Hidden;
                    }
                });
                await Task.Delay(100);
            }
        });
    }

    private void Tab01SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if (sender is TabControl tab)
        {
            if (tab.SelectedContent is Button btn) { }
        }
    }

    private void Default(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Btn0301OnClick(object sender, RoutedEventArgs e)
    {
        var start = TextBox0301.SelectionStart;
        var length = TextBox0301.SelectionLength;
        Label0301.Content = TextBox0301.SelectedText;
    }

    private void TextBox0301OnSelect(object sender, RoutedEventArgs e) { }

    private void TextBox0301OnSelecte(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            Label0301.Content = textBox.SelectedText;
        }
    }

    private void TextBox1001OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            Label1001.Content =
                $"start:{textBox.SelectionStart} length:{textBox.SelectionLength} text:{textBox.SelectedText}";
        }
    }

    private void Btn1001OnClick(object sender, RoutedEventArgs e)
    {
        // TextBox1001.SelectAll();
        TextBox1001.Select(0, TextBox1001.Text.Length);
        PasswordBox1001.SelectAll();
    }

    private void ListBox1101SelectChange(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox listBox)
        {
            if (listBox.SelectedItem is ListBoxItem item)
            {
                var obj = item.Content;
                // MessageBox.Show(obj.ToString());
            }
        }
    }

    private void ListBox1201SelectChange(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox listBox)
        {
            if (listBox.SelectedItem is Country country)
            {
                var text = string.Join(
                    "\n",
                    country.ToString(),
                    $"Value:{listBox.SelectedValue}",
                    $"Show{listBox.SelectedValuePath}"
                );
                MessageBox.Show(text);
            }
        }
    }

    private void ComboBox1301OnSelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox)
        {
            if (comboBox.SelectedItem is Country country)
            {
                var text = string.Join("\n", country.ToString());
                MessageBox.Show(text);
            }
        }
    }

    private void Btn2201OnClick(object sender, RoutedEventArgs e)
    {
        var text = string.Empty;
        if (Calendar2201.SelectedDate.HasValue)
        {
            var selectTime1 = Calendar2201.SelectedDate.Value;
            text += selectTime1.ToString("yyyy-M-d dddd");
            var s = Calendar2201.SelectedDates;
        }

        if (DatePicker2201.SelectedDate.HasValue)
        {
            var selectTime2 = DatePicker2201.SelectedDate.Value;
            text += selectTime2.ToString("yyyy-M-d dddd");
        }

        TextBlock2201.Text = text;
    }
}
