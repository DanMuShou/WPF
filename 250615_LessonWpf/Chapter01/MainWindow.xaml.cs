using System.Windows;

namespace Chapter01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new GridWindow();
            window1.Owner = this;
            window1.Show();
        }
    }
}