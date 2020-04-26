using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NPSim.Domain;
using NPSim.Domain.Builders;
using NPSim.Domain.PhysicalLayer;
using NPSim.Entities;

namespace NPSim.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMediaManager _mediaManager;
        private readonly IOpenSystemBuilder _openSystemBuilder;
        private readonly IMediaBuilder _mediaBuilder;

        private List<IOpenSystem> _openSystems = new List<IOpenSystem>();

        public MainWindow(IMediaManager mediaManager, IOpenSystemBuilder openSystemBuilder, IMediaBuilder mediaBuilder)
        {
            _mediaManager = mediaManager;
            _openSystemBuilder = openSystemBuilder;
            _mediaBuilder = mediaBuilder;

            InitializeComponent();
        }

        private void ComputerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var computer = _openSystemBuilder.BuildComputer();
            _openSystems.Add(computer);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("/Views/Images/computer.png", UriKind.Relative);
            bitmapImage.EndInit();

            var computerImage = new Image
            {
                Width = 50,
                Height = 50,
                Source = bitmapImage
            };

            var computerTextBlock = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Text = computer.Name
            };


            var computerStackPanel = new StackPanel();
            Canvas.SetLeft(computerStackPanel, SimulationCanvas.ActualWidth / 2);
            Canvas.SetTop(computerStackPanel, SimulationCanvas.ActualHeight / 2);

            computerStackPanel.Children.Add(computerImage);
            computerStackPanel.Children.Add(computerTextBlock);

            SimulationCanvas.Children.Add(computerStackPanel);
        }

        private void PhysicalMediaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var computer1 = (Computer)_openSystems[0];
            var computer2 = (Computer)_openSystems[1];

            var physicalMedia = _mediaBuilder.BuildPhysicalMedia();

            _mediaManager.AttachMediaToConnectionEndpoint(physicalMedia, computer1.NicCollection.First().NetworkInterfaces.First());
            _mediaManager.AttachMediaToConnectionEndpoint(physicalMedia, computer2.NicCollection.First().NetworkInterfaces.First());
        }

        private void SendDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var computer1 = (Computer)_openSystems[0];
            var computer2 = (Computer)_openSystems[1];

            var data = Encoding.ASCII.GetBytes("Hello Network!");

            var address = computer1.NicCollection.First().NetworkInterfaces.First().MacAddress;

            computer1.SendData(address, data);
        }
    }
}
