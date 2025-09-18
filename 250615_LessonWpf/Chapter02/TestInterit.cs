using System.Windows;
using System.Windows.Controls;

namespace Chapter02;

public class TestInterit : Control
{
    public string StuName
    {
        get => (string)GetValue(StuNameProperty);
        set => SetValue(StuNameProperty, value);
    }

    // 继承者
    public static readonly DependencyProperty StuNameProperty =
        DependencyAttributes.StuNameProperty.AddOwner(typeof(TestInterit), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));
}