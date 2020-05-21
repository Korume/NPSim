using System.Windows;
using NPSim.Composition;
using NPSim.Domain;
using NPSim.Views;
using Unity;

namespace NPSim
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = UnityConfig.GetContainer();

            var window = container.Resolve<MainWindow>();
            window.Show();
        }
    }
}
