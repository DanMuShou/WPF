using System.Windows;
using System.Windows.Controls;

namespace ConsumptionRecord.WPF.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region 命令
    public DelegateCommand MinWindowCommand { get; }
    public DelegateCommand MaxWindowCommand { get; }
    public DelegateCommand CloseWindowCommand { get; }
    #endregion

    public MainWindow()
    {
        MinWindowCommand = new DelegateCommand(MinWindow);
        MaxWindowCommand = new DelegateCommand(MaxWindow);
        CloseWindowCommand = new DelegateCommand(CloseWindow);

        InitializeComponent();
    }

    private void MinWindow() => WindowState = WindowState.Minimized;

    private void MaxWindow() =>
        WindowState =
            WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

    private void CloseWindow() => Close();

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        MenuToggleButton.IsChecked = false;
    }
}
