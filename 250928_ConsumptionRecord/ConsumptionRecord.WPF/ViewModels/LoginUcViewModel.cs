namespace ConsumptionRecord.WPF.ViewModels;

public class LoginUcViewModel : BindableBase, IDialogAware
{
    public string Title => "登录";
    public DelegateCommand LoginCommand { get; }
    public DelegateCommand<string> ShowLoginPanelCommand { get; }

    private int _selectedIndex;
    public int SelectedIndex
    {
        get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
    }

    private string _pwd;
    public string Pwd
    {
        get => _pwd;
        set => SetProperty(ref _pwd, value);
    }

    public LoginUcViewModel()
    {
        LoginCommand = new DelegateCommand(Login);
        ShowLoginPanelCommand = new DelegateCommand<string>(ShowLoginPanel);
    }

    private void ShowLoginPanel(string index)
    {
        SelectedIndex = int.TryParse(index, out var num) ? num : 0;
    }

    private void Login()
    {
        var pas = Pwd;
        RequestClose.Invoke(new DialogResult(ButtonResult.OK));
    }

    #region IDialogAware
    public DialogCloseListener RequestClose { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters) { }

    #endregion
}
