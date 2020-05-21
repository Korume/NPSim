using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NPSim.Domain.Builders;
using NPSim.Models;
using NPSim.Views;

namespace NPSim.ViewModels
{
    [DataContract]
    public class OpenSystemVm
    {
        public OpenSystemModel OpenSystemModel { get; private set; }

        public string DisplayName { get => OpenSystemModel.OpenSystem.Name; }
        public Point Position { get; set; }

        public event MouseButtonEventHandler MouseLeftButtonDown;
        public event MouseButtonEventHandler MouseLeftButtonUp;
        public event MouseEventHandler MouseMove;

        private Point _offset;
        private readonly Canvas _simulationCanvas;
        private readonly IOpenSystemBuilder _openSystemBuilder;

        public OpenSystemVm(Canvas simulationCanvas, IOpenSystemBuilder openSystemBuilder)
        {
            _simulationCanvas = simulationCanvas;
            _openSystemBuilder = openSystemBuilder;

            AddOpenSystem();
        }

        public void AddOpenSystem()
        {
            var computer = _openSystemBuilder.BuildComputer();
            OpenSystemModel = new OpenSystemModel(computer);

            var openSystemUserControl = new OpenSystemUserControl
            {
                DataContext = this
            };
            openSystemUserControl.MouseLeftButtonDown += OpenSystemUiElement_MouseDown;
            openSystemUserControl.MouseLeftButtonUp += OpenSystemUiElement_MouseUp;
            openSystemUserControl.MouseMove += OpenSystemUiElement_MouseMove;

            _simulationCanvas.Children.Add(openSystemUserControl);
        }

        private void OpenSystemUiElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var openSystemUiElement = (UIElement)sender;
            _offset = e.GetPosition(openSystemUiElement);
            openSystemUiElement.CaptureMouse();

            MouseLeftButtonDown?.Invoke(sender, e);
        }

        private void OpenSystemUiElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var openSystemUiElement = (UIElement)sender;
            openSystemUiElement.ReleaseMouseCapture();

            MouseLeftButtonUp?.Invoke(sender, e);
        }

        private void OpenSystemUiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && Mouse.Captured == sender)
            {
                var openSystemUiElement = (UIElement)sender;
                var simulationCanvas = (Canvas)VisualTreeHelper.GetParent(openSystemUiElement);
                var currentPosition = e.GetPosition(simulationCanvas);

                Position = new Point(currentPosition.X - _offset.X, currentPosition.Y - _offset.Y);

                Canvas.SetLeft(openSystemUiElement, currentPosition.X - _offset.X);
                Canvas.SetTop(openSystemUiElement, currentPosition.Y - _offset.Y);
            }

            MouseMove?.Invoke(sender, e);
        }
    }
}
