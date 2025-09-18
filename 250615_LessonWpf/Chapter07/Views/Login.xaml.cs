using System.Windows;
using Chapter07.ViewModels;

namespace Chapter07.Views;

public partial class Login : Window
{
    private LoginViewModel _viewModel = new LoginViewModel();

    public Login()
    {
        InitializeComponent();

        DataContext = _viewModel; // 绑定视图模型
    }
}
