using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.ViewModels;

public class PersonalUcViewModel : BindableBase
{
    #region 属性
    private bool _isDarkTheme;
    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
                ModifyTheme(theme => theme.SetBaseTheme(value ? BaseTheme.Dark : BaseTheme.Light));
        }
    }
    #endregion

    private void ModifyTheme(Action<Theme> action)
    {
        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();
        action(theme);
        paletteHelper.SetTheme(theme);
    }
}
