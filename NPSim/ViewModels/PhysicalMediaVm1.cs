using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
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
    [DataContract]
    public class PhysicalMediaVm1
    {
        public ObservableCollection<PhysicalMediaModel1> PhysicalMediaModels { get; set; }

        private readonly IMediaBuilder _mediaBuilder;
        private readonly IMediaManager _mediaManager;
        private readonly MainWindowVm1 _mainWindowVm;

        public PhysicalMediaVm1(IMediaBuilder mediaBuilder, IMediaManager mediaManager, MainWindowVm1 mainWindowVm)
        {
            _mediaBuilder = mediaBuilder;
            _mediaManager = mediaManager;
            _mainWindowVm = mainWindowVm;

            PhysicalMediaModels = new ObservableCollection<PhysicalMediaModel1>();
        }

        public void AddPhysicalMedia()
        {
            var physicalMedia = _mediaBuilder.BuildPhysicalMedia();
            var physicalMediaModel = new PhysicalMediaModel1(physicalMedia, _mediaManager, _mainWindowVm);
            PhysicalMediaModels.Add(physicalMediaModel);

            var openSystemModels = _mainWindowVm.OpenSystemVm.OpenSystemModels;
            foreach (var openSystemModel in openSystemModels)
            {
                openSystemModel.MouseLeftButtonUp += physicalMediaModel.OpenSystemUiElement_MouseUp;
            }

            _mainWindowVm.SimulationArea.MouseMove += physicalMediaModel.OpenSystemUiElement_MouseMove;

            Application.Current.MainWindow.Cursor = Cursors.Cross;
        }
    }
}
