using System.Windows;
using System.Windows.Controls;

namespace Chapter02;

public class MyPasswordAttach
{
    // Password 不能被继承
    // 附加属性
    // 定义 注册 包装
    public static string GetPasswordProperty(DependencyObject obj) =>
        (string)obj.GetValue(PasswordProperty);

    public static void SetPasswordProperty(DependencyObject obj, string value) =>
        obj.SetValue(PasswordProperty, value);

    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached(
            "Password",
            typeof(string),
            typeof(MyPasswordAttach),
            new PropertyMetadata("", OnMyPasswordChange)
        );

    // 附加属性值改变 修改 PasswordBox 的密码
    private static void OnMyPasswordChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var oldPwd = (string)e.OldValue;
        var newPwd = (string)e.NewValue;

        if (d is PasswordBox pwdBox)
        {
            pwdBox.Password = newPwd;
        }
        else
        {
            throw new Exception("只能附加到PasswordBox");
        }
    }

    // PasswordBox.Password 修改触发附加值 (需要订阅 PasswordChanged)
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.RegisterAttached(
        "IsOpen",
        typeof(bool),
        typeof(MyPasswordAttach),
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

    // 设置为与默认值不同的话保持触发来订阅 PasswordChanged
    private static void OnIsOpenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox pwdBox)
        {
            pwdBox.PasswordChanged -= OnPasswordChange;
            pwdBox.PasswordChanged += OnPasswordChange;
        }
        else
        {
            throw new Exception("只能附加到PasswordBox");
        }
    }

    // 订阅事件 将password值设置到附加属性
    private static void OnPasswordChange(object sender, RoutedEventArgs e)
    {
        var pwdBox = (PasswordBox)sender;
        SetPasswordProperty(pwdBox, pwdBox.Password);
    }
}
