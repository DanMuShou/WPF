using System.Windows;
using CommunityToolkit.Mvvm.Input;
using ProductionMonitoringPlatform.ViewModels;

namespace ProductionMonitoringPlatform.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        Dispatcher.BeginInvoke(
            new Action(() =>
            {
                var viewModel = DataContext as MainWindowViewModel;
                if (viewModel?.ShowDetailUcCommand != null)
                {
                    viewModel.ShowDetailUcCommand.Execute(null);
                }
            }),
            System.Windows.Threading.DispatcherPriority.Loaded
        );
    }

    #region 窗口状态事件


    [RelayCommand]
    private void MinimizeWindow() => WindowState = WindowState.Minimized;

    [RelayCommand]
    private void MaximizeWindow() =>
        WindowState =
            WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

    [RelayCommand]
    private void CloseWindow() => Close();

    #endregion
}
