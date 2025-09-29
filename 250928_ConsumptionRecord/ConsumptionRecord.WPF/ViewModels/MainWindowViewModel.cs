using System.Windows.Controls;

namespace ConsumptionRecord.WPF.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public MainWindowViewModel()
    {
        ShowContentCommand = new DelegateCommand<string>(ShowContentFunc);
    }

    private UserControl _showControl;
    public UserControl ShowControl
    {
        get => _showControl;
        set => SetProperty(ref _showControl, value);
    }

    public DelegateCommand<string> ShowContentCommand { get; set; }

    private void ShowContentFunc(string name)
    {
        Console.WriteLine(name);
    }
}
