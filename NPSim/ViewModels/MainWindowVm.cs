using System.Collections.ObjectModel;
using System.Windows.Controls;
using NPSim.Domain.Builders;
using NPSim.Domain.PhysicalLayer;
using NPSim.Models;
using NPSim.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace NPSim.ViewModels
{
    public class MainWindowVm : BindableBase
    {
        private LogicalCursorState _logicalCursorState = LogicalCursorState.Idle;

        public ObservableCollection<OpenSystemVm> OpenSystemVms { get; }
        public ObservableCollection<PhysicalMediaVm> PhysicalMediaVms { get; }

        public DelegateCommand<Canvas> AddOpenSystemCommand { get; }
        public DelegateCommand<Canvas> AddPhysicalMediaCommand { get; }
        public DelegateCommand OpenTrafficCreatorCommand { get; }

        private readonly IOpenSystemBuilder _openSystemBuilder;
        private readonly IMediaBuilder _mediaBuilder;
        private readonly IMediaManager _mediaManager;

        public MainWindowVm(IOpenSystemBuilder openSystemBuilder, IMediaBuilder mediaBuilder, IMediaManager mediaManager)
        {
            _openSystemBuilder = openSystemBuilder;
            _mediaBuilder = mediaBuilder;
            _mediaManager = mediaManager;

            OpenSystemVms = new ObservableCollection<OpenSystemVm>();
            PhysicalMediaVms = new ObservableCollection<PhysicalMediaVm>();

            AddOpenSystemCommand = new DelegateCommand<Canvas>(c => { AddOpenSystem(c); });
            AddPhysicalMediaCommand = new DelegateCommand<Canvas>(c => { AddPhysicalMedia(c); });
            OpenTrafficCreatorCommand = new DelegateCommand(() => { OpenTrafficCreator(); });
        }

        private void AddOpenSystem(Canvas simulationCanvas)
        {
            var openSystemVm = new OpenSystemVm(simulationCanvas, _openSystemBuilder);
            OpenSystemVms.Add(openSystemVm);
        }

        private void AddPhysicalMedia(Canvas simulationCanvas)
        {
            var physicalMediaVm = new PhysicalMediaVm(simulationCanvas, _mediaBuilder, _mediaManager);

            foreach (var openSystemVm in OpenSystemVms)
            {
                physicalMediaVm.AttachMouseEvents(openSystemVm);
            }

            PhysicalMediaVms.Add(physicalMediaVm);
        }

        private void OpenTrafficCreator()
        {
            var trafficWindow = new TrafficWindow
            {
                DataContext = new TrafficWindowVm(this)
            };
            trafficWindow.Show();
        }
    }
}
