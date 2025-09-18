using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chapter15;

public class MCustomListView : ViewBase
{
    /// <summary>
    /// 获取或设置项模板，用于定义每个项的显示方式
    /// </summary>
    private DataTemplate _itemTemplate;
    public DataTemplate ItemTemplate
    {
        get => _itemTemplate;
        set => _itemTemplate = value;
    }

    /// <summary>
    /// 获取或设置选中项的背景画刷
    /// </summary>
    private Brush _selectedBackground = Brushes.Transparent;
    public Brush SelectedBackground
    {
        get => _selectedBackground;
        set => _selectedBackground = value;
    }

    /// <summary>
    /// 获取或设置选中项的边框画刷
    /// </summary>
    private Brush _selectedBorderBrush = Brushes.Black;
    public Brush SelectedBorderBrush
    {
        get => _selectedBorderBrush;
        set => _selectedBorderBrush = value;
    }

    /// <summary>
    /// 获取默认样式键，用于指定控件的默认样式资源
    /// </summary>
    protected override object DefaultStyleKey => new ComponentResourceKey(GetType(), "TileView");

    /// <summary>
    /// 获取项容器的默认样式键，用于指定项容器的默认样式资源
    /// </summary>
    protected override object ItemContainerDefaultStyleKey =>
        new ComponentResourceKey(GetType(), "TileViewItem");
}
