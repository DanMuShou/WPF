using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Chapter14;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<MSheng> _mShengs = [];

    public MainWindow()
    {
        InitializeComponent();

        for (var sheng = 0; sheng < 10; sheng++)
        {
            var mSheng = new MSheng(sheng, []);
            _mShengs.Add(mSheng);
            for (var shi = 0; shi < 20; shi++)
            {
                var mShi = new MShi(shi, []);
                mSheng.AddShi(mShi);
                for (var xian = 0; xian < 30; xian++)
                {
                    mShi.AddXian(xian);
                }
            }
        }

        DataContext = new Product()
        {
            Name = "Product",
            Price = 3.0m,
            Count = 1000,
        };
    }

    private void ComboBox0101_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox)
        {
            var index = comboBox.SelectedIndex;

            var obj = comboBox.SelectedItem;
            if (obj is MSheng mSheng)
            {
                ComboBox0102.ItemsSource = mSheng.Shis;
                ComboBox0102.DisplayMemberPath = "Shi";
            }
        }
    }

    private void Btn1001_OnClick(object sender, RoutedEventArgs e)
    {
        if (Validation.GetHasError(TextBox1001))
        {
            MessageBox.Show("请输入数字");
        }
    }

    private void Btn1201_OnClick(object sender, RoutedEventArgs e)
    {
        if (BindingGroup1201.CommitEdit())
        {
            MessageBox.Show("保存成功");
        }
        else
        {
            MessageBox.Show("保存失败");
        }
    }
}

class MSheng(int sheng, List<MShi> shis)
{
    public int Sheng { get; } = sheng;
    public List<MShi> Shis { get; } = shis;

    public void AddShi(MShi shi)
    {
        Shis.Add(shi);
    }
}

class MShi(int shi, List<int> xians)
{
    public int Shi { get; } = shi;

    public void AddXian(int xian)
    {
        xians.Add(xian);
    }
}

class Product() : IDataErrorInfo
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }

    public string Error { get; }

    /// <summary>
    /// 获取错误信息
    /// </summary>
    /// <param name="columnName">属性名字</param>
    public string this[string columnName]
    {
        get
        {
            switch (columnName)
            {
                case nameof(Name):
                    if (string.IsNullOrEmpty(Name))
                        return "名称不能为空";
                    break;
                case nameof(Price):
                    if (Price <= 0)
                        return "价格必须大于0";
                    break;
            }
            return string.Empty;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
