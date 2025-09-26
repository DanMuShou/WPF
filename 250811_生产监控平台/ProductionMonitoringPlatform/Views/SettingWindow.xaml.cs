using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;

namespace ProductionMonitoringPlatform.Views;

public partial class SettingWindow : Window
{
    public SettingWindow()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void JumpToTitle(object tag)
    {
        InfoFrame.Navigate(
            new Uri(
                "pack://application:,,,/ProductionMonitoringPlatform;component/Views/SettingPage.xaml#"
                    + tag,
                UriKind.Absolute
            )
        );
    }
}
