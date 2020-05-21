using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NPSim.Entities;

namespace NPSim.Models
{
    [DataContract]
    public class OpenSystemModel1
    {
        private Point _offset;
        private Point _position;

        [DataMember]
        public BaseOpenSystem OpenSystem { get; set; }

        [DataMember]
        public Point Position { get; private set; }

        public string DisplayName { get => OpenSystem.Name; }

        public event MouseButtonEventHandler MouseLeftButtonDown;
        public event MouseButtonEventHandler MouseLeftButtonUp;
        public event MouseEventHandler MouseMove;

        public OpenSystemModel1(BaseOpenSystem openSystem)
        {
            OpenSystem = openSystem;
        }

        public void OpenSystemUiElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var openSystemUiElement = (UIElement)sender;
            _offset = e.GetPosition(openSystemUiElement);
            openSystemUiElement.CaptureMouse();

            MouseLeftButtonDown?.Invoke(sender, e);
        }

        public void OpenSystemUiElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var openSystemUiElement = (UIElement)sender;
            openSystemUiElement.ReleaseMouseCapture();

            MouseLeftButtonUp?.Invoke(sender, e);
        }

        public void OpenSystemUiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && Mouse.Captured == sender)
            {
                var openSystemUiElement = (UIElement)sender;
                var simulationCanvas = (Canvas)VisualTreeHelper.GetParent(openSystemUiElement);
                var currentPosition = e.GetPosition(simulationCanvas);

                Position = new Point(currentPosition.X - _offset.X, currentPosition.Y - _offset.Y);

                Canvas.SetLeft(openSystemUiElement, Position.X);
                Canvas.SetTop(openSystemUiElement, Position.Y);
            }

            MouseMove?.Invoke(sender, e);
        }
    }
}
