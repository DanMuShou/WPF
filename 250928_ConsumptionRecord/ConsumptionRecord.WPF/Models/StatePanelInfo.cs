using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.Models;

public class StatePanelInfo : BindableBase
{
    public string Title { get; set; }

    private string _result;
    public string Result
    {
        get => _result;
        set => SetProperty(ref _result, value);
    }
    public string BackColor { get; set; }
    public PackIconKind Icon { get; set; }
    public Type? ViewType { get; set; }
    public NavigationParameters? Parameters { get; set; }
}
