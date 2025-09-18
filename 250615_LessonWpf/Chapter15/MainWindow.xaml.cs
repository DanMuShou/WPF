using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Chapter15.Models;
using Chapter15.Services;

namespace Chapter15;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ListCollectionView _view;

    public MainWindow()
    {
        InitializeComponent();

        var service = new ProductService(new ProductContext());
        var products = service.GetProducts();

        _view = (CollectionViewSource.GetDefaultView(products) as ListCollectionView)!; // 列表数据提供视图功能，允许在不影响源数据的情况下对数据进行排序、筛选和分组

        var priceRangeGroup = new ProductPriceGrouper();

        _view.CurrentChanged += ViewOnCurrentChanged;
        _view.SortDescriptions.Add(
            new SortDescription(nameof(Product.Name), ListSortDirection.Ascending)
        );
        _view.GroupDescriptions!.Add(
            new PropertyGroupDescription(nameof(Product.Price), priceRangeGroup)
        );

        DataContext = _view;
        ViewOnCurrentChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// 改变现实的条数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ViewOnCurrentChanged(object? sender, EventArgs e)
    {
        TbPagePosition.Text = $"{_view.CurrentPosition + 1} / {_view.Count}";
        BtnMoveLeftItem.IsEnabled = _view.CurrentPosition > 0;
        BtnMoveRightItem.IsEnabled = _view.CurrentPosition < _view.Count - 1;
    }

    private void BtnMoveLeftItem_OnClick(object sender, RoutedEventArgs e)
    {
        _view.MoveCurrentToPrevious(); // 移动到前一项
    }

    private void BtnMoveRightItem_OnClick(object sender, RoutedEventArgs e)
    {
        _view.MoveCurrentToNext();
    }

    private void CbProducts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox { SelectedItem: Product product })
        {
            _view.MoveCurrentTo(product);
        }
    }

    private void BtnFilterProduct_OnClick(object sender, RoutedEventArgs e)
    {
        _view.Filter = PriceFilter;
    }

    private void BtnRemoveFilter_OnClick(object sender, RoutedEventArgs e)
    {
        _view.Filter = null;
    }

    private bool PriceFilter(object obj)
    {
        if (
            obj is Product product
            && decimal.TryParse(TbFilterPriceNumber.Text, out var inputPrice)
        )
        {
            return product.Price >= inputPrice;
        }
        return false;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is Selector { SelectedItem: Product product })
        {
            _view.MoveCurrentTo(product);
        }
    }
}
