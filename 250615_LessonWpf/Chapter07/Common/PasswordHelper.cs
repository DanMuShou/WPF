using System.Windows;
using System.Windows.Controls;

namespace Chapter07.Common;

/// <summary>
/// 自定义属性 - 绑定密码
/// </summary>
public class PasswordHelper
{
    /// <summary>
    /// 注册附加依赖属性"Password"来存储密码，类型为string
    /// </summary>
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached(
            "Password",
            typeof(string),
            typeof(PasswordHelper),
            new PropertyMetadata(null, OnMyPasswordChange)
        );

    public static void SetPassword(DependencyObject element, string value)
    {
        element.SetValue(PasswordProperty, value);
    }

    public static string GetPassword(DependencyObject element)
    {
        return (string)element.GetValue(PasswordProperty);
    }

    private static void OnMyPasswordChange(
        DependencyObject sender,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (sender is not PasswordBox pwdBox)
            return;

        var newPwd = (string)e.NewValue;
        if (pwdBox.Password != newPwd)
        {
            pwdBox.Password = newPwd;
        }
    }

    /// <summary>
    /// 注册附加依赖属性"IsOpen"来检测修改状态，类型为bool
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.RegisterAttached(
        "IsOpen",
        typeof(bool),
        typeof(PasswordHelper),
        new PropertyMetadata(false, OnIsOpenChange)
    );

    public static void SetIsOpen(DependencyObject element, bool value)
    {
        element.SetValue(IsOpenProperty, value);
    }

    public static bool GetIsOpen(DependencyObject element)
    {
        return (bool)element.GetValue(IsOpenProperty);
    }

    /// <summary>
    ///  附加属性"IsOpen"的修改状态监听
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <exception cref="Exception"></exception>
    private static void OnIsOpenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            passwordBox.PasswordChanged -= OnPasswordChange;
            passwordBox.PasswordChanged += OnPasswordChange;
        }
        else
        {
            throw new Exception("只能附加到PasswordBox");
        }
    }

    /// <summary>
    /// 密码修改状态监听
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnPasswordChange(object sender, RoutedEventArgs e)
    {
        var pwdBox = (PasswordBox)sender;
        SetPassword(pwdBox, pwdBox.Password);
    }
}
