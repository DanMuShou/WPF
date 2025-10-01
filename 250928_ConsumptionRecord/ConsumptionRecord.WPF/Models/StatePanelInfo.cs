using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.Models;

public class StatePanelInfo()
{
    public string Title { get; set; }
    public string Result { get; set; }
    public PackIconKind Icon { get; set; }
    public string BackColor { get; set; }
    public string ViewName { get; set; }
}
