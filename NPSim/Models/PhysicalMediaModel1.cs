using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NPSim.Domain.PhysicalLayer;
using NPSim.Entities.PhysicalLayer.Media;
using NPSim.ViewModels;
using NPSim.Views;

namespace NPSim.Models
{
    [DataContract]
    public class PhysicalMediaModel1 : INotifyPropertyChanged
    {
        private readonly IMediaManager _mediaManager;
        private readonly MainWindowVm1 _mainWindowVm;

        [DataMember]
        private Point? _from;

        [DataMember]
        private Point? _to;

        [DataMember]
        public PhysicalMedia PhysicalMedia { get; }

        public Point? From
        {
            get => _from;
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public Point? To
        {
            get => _to;
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PhysicalMediaModel1(PhysicalMedia physicalMedia, IMediaManager mediaManager, MainWindowVm1 mainWindowVm)
        {
            PhysicalMedia = physicalMedia;
            _mediaManager = mediaManager;
            _mainWindowVm = mainWindowVm;
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void OnOpenSystemMove()
        {
            throw new NotImplementedException();
        }

        public void OpenSystemUiElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel OpenSystemCanvasElement)
            {
                var openSystemModel = (OpenSystemModel1)OpenSystemCanvasElement.DataContext;
                var connectionEndpoint = openSystemModel.OpenSystem.GetAvailableNetworkInterfaces().FirstOrDefault();

                _mediaManager.AttachMediaToConnectionEndpoint(PhysicalMedia, connectionEndpoint);

                if (PhysicalMedia.HasAvailableConnector)
                {
                    From = GetCenterOfUiElement(OpenSystemCanvasElement);
                    To = From;
                }
                else
                {
                    To = GetCenterOfUiElement(OpenSystemCanvasElement);

                    var openSystemModels = _mainWindowVm.OpenSystemVm.OpenSystemModels;
                    foreach (var item in openSystemModels)
                    {
                        item.MouseLeftButtonUp -= OpenSystemUiElement_MouseUp;
                    }

                    _mainWindowVm.SimulationArea.MouseMove -= OpenSystemUiElement_MouseMove;

                    Application.Current.MainWindow.Cursor = Cursors.Arrow;
                }
            }
        }

        public void OpenSystemUiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (From.HasValue)
            {
                var simulationArea = (UIElement)sender;
                To = e.GetPosition(simulationArea);
            }
        }

        private Point GetCenterOfUiElement(UIElement element)
        {
            return new Point(Canvas.GetLeft(element) + element.RenderSize.Width / 2, Canvas.GetTop(element) + element.RenderSize.Height / 2);
        }
    }
}
