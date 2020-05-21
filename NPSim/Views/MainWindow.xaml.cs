using System.Windows;
using NPSim.ViewModels;

namespace NPSim.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowVm1 mainWindowVm)
        {
            InitializeComponent();

            mainWindowVm.SimulationArea = SimulationArea;
            DataContext = mainWindowVm;

            //OpenSystemUserControls.ItemsSource = mainWindowVm.OpenSystemVm.OpenSystemModels;
            PhysicalMediaUserControls.ItemsSource = mainWindowVm.PhysicalMediaVm.PhysicalMediaModels;
        }
    }
}
