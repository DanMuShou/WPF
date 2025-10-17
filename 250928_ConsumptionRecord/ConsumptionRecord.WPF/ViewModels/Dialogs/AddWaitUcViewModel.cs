using System.Windows;
using ConsumptionRecord.WPF.Models;
using ConsumptionRecord.WPF.Services;
using MaterialDesignThemes.Wpf;

namespace ConsumptionRecord.WPF.ViewModels.Dialogs;

public class AddWaitUcViewModel : BindableBase, IDialogHostAware
{
    #region 属性
    private WaitInfo _waitInfo = new();
    public WaitInfo WaitInfo
    {
        get => _waitInfo;
        set => SetProperty(ref _waitInfo, value);
    }

    private int _selectedIndex;
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            SetProperty(ref _selectedIndex, value);
            WaitInfo.IsDone = value == 1;
        }
    }
    #endregion

    public AddWaitUcViewModel()
    {
        SaveCommand = new DelegateCommand(Save);
        CancelCommand = new DelegateCommand(Cancel);
    }

    private void Save()
    {
        if (
            string.IsNullOrWhiteSpace(WaitInfo.Title) || string.IsNullOrWhiteSpace(WaitInfo.Content)
        )
        {
            MessageBox.Show("信息填写不完整");
            return;
        }

        if (DialogHost.IsDialogOpen("RootDialog"))
        {
            var dialogResult = new DialogResult(ButtonResult.OK)
            {
                Parameters = new DialogParameters { { WaitInfo.GetType().Name, WaitInfo } },
            };
            DialogHost.Close("RootDialog", dialogResult);
        }
    }

    private void Cancel()
    {
        if (DialogHost.IsDialogOpen("RootDialog"))
        {
            DialogHost.Close("RootDialog", new DialogResult(ButtonResult.Cancel));
        }
    }

    #region IDialogHostAware

    public void OnDialogOpening(IDialogParameters parameters)
    {
        if (parameters.TryGetValue<WaitInfo>(nameof(WaitInfo), out var waitInfo))
            WaitInfo = waitInfo;
    }

    public DelegateCommand SaveCommand { get; }
    public DelegateCommand CancelCommand { get; }
    #endregion
}
