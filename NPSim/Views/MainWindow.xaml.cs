using System.Windows;
using NPSim.ViewModels;

namespace NPSim.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowVm mainWindowVm)
        {
            InitializeComponent();

            DataContext = mainWindowVm;
        }
    }
}
