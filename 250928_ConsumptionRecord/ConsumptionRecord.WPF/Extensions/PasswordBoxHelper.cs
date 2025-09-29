using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace ConsumptionRecord.WPF.Extensions;

/// <summary>
/// PasswordBoxHelper类提供对PasswordBox控件的密码属性进行依赖属性绑定的支持
/// </summary>
public class PasswordBoxHelper
{
    /// <summary>
    /// 定义Pwd依赖属性，用于绑定PasswordBox的密码值
    /// </summary>
    public static readonly DependencyProperty PwdProperty = DependencyProperty.RegisterAttached(
        "Pwd",
        typeof(string),
        typeof(PasswordBoxHelper),
        new PropertyMetadata(string.Empty, OnPwdChanged)
    );

    /// <summary>
    /// 设置指定DependencyObject的Pwd属性值
    /// </summary>
    /// <param name="dp">要设置属性的DependencyObject对象</param>
    /// <param name="value">要设置的密码字符串值</param>
    public static void SetPwd(DependencyObject dp, string value) => dp.SetValue(PwdProperty, value);

    /// <summary>
    /// 获取指定DependencyObject的Pwd属性值
    /// </summary>
    /// <param name="dp">要获取属性的DependencyObject对象</param>
    /// <returns>返回当前设置的密码字符串值</returns>
    public static string GetPwd(DependencyObject dp) => (string)dp.GetValue(PwdProperty);

    /// <summary>
    /// 当Pwd依赖属性值发生变化时的回调方法，同步更新PasswordBox的Password属性
    /// </summary>
    /// <param name="d">发生属性变化的DependencyObject对象</param>
    /// <param name="e">包含属性变化信息的事件参数</param>
    private static void OnPwdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox)
            return;

        // 获取新的密码值并同步到PasswordBox控件
        var newPassword = (string)e.NewValue;
        if (passwordBox.Password != newPassword)
            passwordBox.Password = newPassword;
    }
}

/// <summary>
/// PasswordBoxBehavior类是一个行为类，用于扩展PasswordBox控件的功能
/// 该行为监听PasswordBox的密码变化事件，并同步更新PasswordBoxHelper中的密码值
/// </summary>
public class PasswordBoxBehavior : Behavior<PasswordBox>
{
    /// <summary>
    /// 当行为附加到PasswordBox控件时调用此方法
    /// 注册PasswordChanged事件处理程序以监听密码变化
    /// </summary>
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PasswordChanged += OnPwdChange;
    }

    /// <summary>
    /// 当行为从PasswordBox控件分离时调用此方法
    /// 取消注册PasswordChanged事件处理程序以防止内存泄漏
    /// </summary>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PasswordChanged -= OnPwdChange;
    }

    /// <summary>
    /// 处理PasswordBox密码变化事件的方法
    /// 当密码发生变化时，同步更新PasswordBoxHelper中的密码值
    /// </summary>
    /// <param name="sender">触发事件的对象，应为PasswordBox实例</param>
    /// <param name="e">路由事件参数</param>
    private void OnPwdChange(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox passwordBox)
            return;

        // 获取当前PasswordBoxHelper中存储的密码
        var password = PasswordBoxHelper.GetPwd(passwordBox);

        // 如果PasswordBox中的密码与Helper中存储的密码不一致，则更新Helper中的值
        if (passwordBox.Password != password)
            PasswordBoxHelper.SetPwd(passwordBox, passwordBox.Password);
    }
}
