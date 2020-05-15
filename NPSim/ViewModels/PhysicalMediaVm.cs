using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using NPSim.Domain.Builders;
using NPSim.Domain.PhysicalLayer;
using NPSim.Models;
using NPSim.Views;

namespace NPSim.ViewModels
{
    public class PhysicalMediaVm : INotifyPropertyChanged
    {
        private Point? _from;
        private Point? _to;

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

        public PhysicalMediaModel PhysicalMediaModel { get; set; }

        private LogicalCursorState _logicalCursorState = LogicalCursorState.Idle;

        private readonly Canvas _simulationCanvas;
        private readonly IMediaBuilder _mediaBuilder;
        private readonly IMediaManager _mediaManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public PhysicalMediaVm(Canvas simulationCanvas, IMediaBuilder mediaBuilder, IMediaManager mediaManager)
        {
            _simulationCanvas = simulationCanvas;
            _mediaBuilder = mediaBuilder;
            _mediaManager = mediaManager;

            AddPhysicalMedia();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void OnOpenSystemMove()
        {

        }

        public void AttachMouseEvents(OpenSystemVm openSystemVm)
        {
            openSystemVm.MouseLeftButtonUp += OpenSystemUiElement_MouseUp;
            Application.Current.MainWindow.MouseMove += OpenSystemUiElement_MouseMove;
        }

        private void AddPhysicalMedia()
        {
            var physicalMedia = _mediaBuilder.BuildPhysicalMedia();
            PhysicalMediaModel = new PhysicalMediaModel(physicalMedia);

            var physicalMediaUserControl = new PhysicalMediaUserControl
            {
                DataContext = this
            };
            Panel.SetZIndex(physicalMediaUserControl, -1);

            _simulationCanvas.Children.Add(physicalMediaUserControl);

            _logicalCursorState = LogicalCursorState.PhysicalMediaCreation;
            Application.Current.MainWindow.Cursor = Cursors.Cross;
        }

        private void OpenSystemUiElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is OpenSystemUserControl openSystemUserControl && _logicalCursorState == LogicalCursorState.PhysicalMediaCreation)
            {
                var openSystemVm = (OpenSystemVm)openSystemUserControl.DataContext;
                var connectionEndpoint = openSystemVm.OpenSystemModel.OpenSystem.GetAvailableNetworkInterfaces().FirstOrDefault();

                _mediaManager.AttachMediaToConnectionEndpoint(PhysicalMediaModel.PhysicalMedia, connectionEndpoint);

                if (PhysicalMediaModel.PhysicalMedia.HasAvailableConnector)
                {
                    From = GetCenterOfUiElement(openSystemUserControl);
                    To = From;
                }
                else
                {
                    To = GetCenterOfUiElement(openSystemUserControl);

                    _logicalCursorState = LogicalCursorState.Idle;
                    Application.Current.MainWindow.Cursor = Cursors.Arrow;
                }
            }
        }

        public Point GetCenterOfUiElement(UIElement element)
        {
            return new Point(Canvas.GetLeft(element) + element.RenderSize.Width / 2, Canvas.GetTop(element) + element.RenderSize.Height / 2);
        }

        private void OpenSystemUiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (_logicalCursorState == LogicalCursorState.PhysicalMediaCreation)
            {
                if (From.HasValue)
                {
                    To = Mouse.GetPosition(_simulationCanvas);
                }
            }
        }
    }
}
