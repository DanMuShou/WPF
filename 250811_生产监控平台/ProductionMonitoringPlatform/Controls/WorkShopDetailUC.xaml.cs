using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.Input;

namespace ProductionMonitoringPlatform.Controls;

public partial class WorkShopDetailUC : UserControl
{
    public WorkShopDetailUC()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void OpenDetailPanel()
    {
        AllDetailPanel.Visibility = Visibility.Visible;
        var animationTime = TimeSpan.FromSeconds(0.3);
        var marginAni = new ThicknessAnimation(
            new Thickness(0, 50, 0, -50),
            new Thickness(0, 0, 0, 0),
            new Duration(animationTime)
        );
        var opacityAni = new DoubleAnimation(0, 1, new Duration(animationTime));

        Storyboard.SetTarget(marginAni, DetailPanel);
        Storyboard.SetTarget(opacityAni, DetailPanel);
        Storyboard.SetTargetProperty(marginAni, new PropertyPath(nameof(DetailPanel.Margin)));
        Storyboard.SetTargetProperty(opacityAni, new PropertyPath(nameof(DetailPanel.Opacity)));
        var storyboard = new Storyboard();
        storyboard.Children.Add(marginAni);
        storyboard.Children.Add(opacityAni);
        storyboard.Begin();
    }

    [RelayCommand]
    private void CloseDetailPanel()
    {
        var animationTime = TimeSpan.FromSeconds(0.3);
        var marginAni = new ThicknessAnimation(
            new Thickness(0, 0, 0, 0),
            new Thickness(0, 50, 0, -50),
            new Duration(animationTime)
        );
        var opacityAni = new DoubleAnimation(1, 0, new Duration(animationTime));

        Storyboard.SetTarget(marginAni, DetailPanel);
        Storyboard.SetTarget(opacityAni, DetailPanel);
        Storyboard.SetTargetProperty(marginAni, new PropertyPath(nameof(DetailPanel.Margin)));
        Storyboard.SetTargetProperty(opacityAni, new PropertyPath(nameof(DetailPanel.Opacity)));
        var storyboard = new Storyboard();
        storyboard.Children.Add(marginAni);
        storyboard.Children.Add(opacityAni);
        storyboard.Completed += (_, _) =>
        {
            AllDetailPanel.Visibility = Visibility.Hidden;
        };
        storyboard.Begin();
    }
}
