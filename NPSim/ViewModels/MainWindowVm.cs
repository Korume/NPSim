using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using NPSim.Domain.Builders;
using NPSim.Domain.PhysicalLayer;
using NPSim.Models;
using NPSim.Sharing;
using NPSim.Sharing.Client;
using NPSim.Sharing.Lib;
using NPSim.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace NPSim.ViewModels
{
    public class MainWindowVm : BindableBase
    {
        private LogicalCursorState _logicalCursorState = LogicalCursorState.Idle;
        public NetworkProjectModel NetworkProjectModel { get; private set; }

        /**/public NetworkProjectModel1 NetworkProjectModel1 { get; private set; }

        private ServiceHost _serviceHost;
        private SharingServiceClient _sharingServiceClient;
        private SharingServiceCallback _sharingServiceCallback;

        private readonly IOpenSystemBuilder _openSystemBuilder;
        private readonly IMediaBuilder _mediaBuilder;
        private readonly IMediaManager _mediaManager;

        public DelegateCommand<Canvas> AddOpenSystemCommand { get; }
        public DelegateCommand<Canvas> AddPhysicalMediaCommand { get; }
        public DelegateCommand OpenTrafficWindowCommand { get; }
        public DelegateCommand OpenAccessCommand { get; }
        public DelegateCommand OpenSharingWindowCommand { get; }

        public MainWindowVm(IOpenSystemBuilder openSystemBuilder, IMediaBuilder mediaBuilder, IMediaManager mediaManager)
        {
            _openSystemBuilder = openSystemBuilder;
            _mediaBuilder = mediaBuilder;
            _mediaManager = mediaManager;

            LoadOrCreateNewProject();

            AddOpenSystemCommand = new DelegateCommand<Canvas>(c => { AddOpenSystem(c); });
            AddPhysicalMediaCommand = new DelegateCommand<Canvas>(c => { AddPhysicalMedia(c); });
            OpenTrafficWindowCommand = new DelegateCommand(() => { OpenTrafficWindow(); });
            OpenAccessCommand = new DelegateCommand(() => { OpenAccess(); });
            OpenSharingWindowCommand = new DelegateCommand(() => { OpenSharingWindow(); });
        }

        public void LoadOrCreateNewProject(NetworkProjectModel networkProjectModel = null)
        {
            NetworkProjectModel = networkProjectModel ?? new NetworkProjectModel();
        }

        private void AddOpenSystem(Canvas simulationCanvas)
        {
            var openSystemVm = new OpenSystemVm(simulationCanvas, _openSystemBuilder);
            NetworkProjectModel.OpenSystemVms.Add(openSystemVm);
        }

        private void AddPhysicalMedia(Canvas simulationCanvas)
        {
            var physicalMediaVm = new PhysicalMediaVm(simulationCanvas, _mediaBuilder, _mediaManager);

            foreach (var openSystemVm in NetworkProjectModel.OpenSystemVms)
            {
                physicalMediaVm.AttachMouseEvents(openSystemVm);
            }

            NetworkProjectModel.PhysicalMediaVms.Add(physicalMediaVm);
        }

        private void OpenTrafficWindow()
        {
            //var trafficWindow = new TrafficWindow
            //{
            //    DataContext = new TrafficWindowVm(this)
            //};
            //trafficWindow.Show();
        }

        private void OpenAccess()
        {
            if (_serviceHost == null)
            {
                _serviceHost = new ServiceHost(typeof(SharingService));
                _serviceHost.Open();
            }
        }

        private void OpenSharingWindow()
        {
            //var uiSyncContext = SynchronizationContext.Current;

            //Task.Factory.StartNew(() =>
            //{
            //    _sharingServiceCallback = new SharingServiceCallback(uiSyncContext, this);
            //    _sharingServiceClient = new SharingServiceClient(new InstanceContext(_sharingServiceCallback), "ServerEndpoint");
            //    _sharingServiceClient.Open();

            //    _sharingServiceClient.ConnectToSharedProject();
            //});
        }
    }
}
