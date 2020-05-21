using System.Windows;
using System.Windows.Controls;
using NPSim.Models;

namespace NPSim.Views
{
    public partial class OpenSystemUserControl : UserControl
    {
        public OpenSystemUserControl()
        {
            InitializeComponent();
        }

        private void OpenSystemCanvasElement_Loaded(object sender, RoutedEventArgs e)
        {
            var openSystemModel = (OpenSystemModel1)DataContext;
            var openSystemCanvasElement = (UIElement)sender;

            openSystemCanvasElement.MouseLeftButtonDown += openSystemModel.OpenSystemUiElement_MouseDown;
            openSystemCanvasElement.MouseLeftButtonUp += openSystemModel.OpenSystemUiElement_MouseUp;
            openSystemCanvasElement.MouseMove += openSystemModel.OpenSystemUiElement_MouseMove;

            Canvas.SetLeft(openSystemCanvasElement, openSystemModel.Position.X);
            Canvas.SetTop(openSystemCanvasElement, openSystemModel.Position.Y);
        }
    }
}
