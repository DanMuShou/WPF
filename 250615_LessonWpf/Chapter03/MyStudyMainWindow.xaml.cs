using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Chapter03;

public partial class MyStudyMainWindow : Window
{
    public MyStudyMainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 拖拽显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LabelOnDrop(object sender, DragEventArgs e) =>
        ((Label)sender).Content = e.Data.GetData(DataFormats.Text)?.ToString();

    /// <summary>
    /// 拖拽
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LabelOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Label label)
            return;
        DragDrop.DoDragDrop(label, label.Content, DragDropEffects.Copy);
    }

    private void TextBoxOnKeyDown(object sender, KeyEventArgs e)
    {
        var route = e.RoutedEvent;
        if (e.Key == Key.Enter)
        {
            // TODO
        }
    }

    /// <summary>
    /// 隧道事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TextBoxOnPreTextInput(object sender, TextCompositionEventArgs e)
    {
        var str = e.Text; // 输入的字符
    }

    private void BtnOnClick(object sender, RoutedEventArgs e)
    {
        Text01.Focus();
    }

    private void Btn13OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ListBox12OnClick(object sender, RoutedEventArgs e)
    {
        // 获取最初的源 冒泡事件
        if (e.OriginalSource is not CheckBox checkBox)
            return;
        if (checkBox.IsChecked is true)
        {
            Label12.Content = $"第{ListBox12.SelectedIndex}项: {checkBox.Content}";
        }
        else
        {
            Label12.Content = string.Empty;
        }
    }

    /// <summary>
    /// 获取所有选项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Buton12OnClick(object sender, RoutedEventArgs e)
    {
        var result = string.Empty;

        foreach (var item in ListBox12.Items)
        {
            if (item is CheckBox { IsChecked: true } checkBox)
            {
                result += $"{checkBox.Content}-";
            }

            Label12.Content = result.TrimEnd('-');
        }
    }

    private void Btn1301OnClick(object sender, RoutedEventArgs e)
    {
        var res = string.Empty;
        foreach (var item in StackPanel1301.Children)
        {
            if (item is RadioButton { IsChecked: true } radioButton)
            {
                res += $"{radioButton.Content}-";
            }
        }
        MessageBox.Show(res.TrimEnd('-'));
    }

    private void RadioBtn12OnClick(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is RadioButton { IsChecked: true } checkBox)
        {
            MessageBox.Show($"我选择了{checkBox.Content}");
        }
    }

    private void RadioBtn13OnIndeter(object sender, RoutedEventArgs e) { }

    private void Run21OnMouseEnter(object sender, MouseEventArgs e)
    {
        Popup21.IsOpen = true;
    }

    private void Link21OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is Hyperlink link)
        {
            var url = link.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }

    private void Btn23OnClick(object sender, RoutedEventArgs e)
    {
        ScrollViewer23.LineUp();
    }

    private void Btn2301OnClick(object sender, RoutedEventArgs e)
    {
        ScrollViewer23.LineDown();
    }

    private void Btn2302OnClick(object sender, RoutedEventArgs e)
    {
        ScrollViewer23.PageUp();
    }

    private void Btn2303OnClick(object sender, RoutedEventArgs e)
    {
        ScrollViewer23.PageDown();
    }
}
