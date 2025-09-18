using System.Windows.Controls;

namespace Chapter12;

public partial class Bome : UserControl
{
    /// <summary>
    /// 坠落状态
    /// </summary>
    public bool IsFalling { get; set; } = false;

    public Bome()
    {
        InitializeComponent();
    }
}
