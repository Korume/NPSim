using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
    public class MainWindowVm1 : BindableBase
    {
        public OpenSystemVm1 OpenSystemVm { get; private set; }
        public PhysicalMediaVm1 PhysicalMediaVm { get; private set; }

        public NetworkProjectModel1 NetworkProjectModel { get; private set; }

        private ServiceHost _serviceHost;
        private SharingServiceClient _sharingServiceClient;
        private SharingServiceCallback _sharingServiceCallback;

        private readonly IOpenSystemBuilder _openSystemBuilder;
        private readonly IMediaBuilder _mediaBuilder;
        private readonly IMediaManager _mediaManager;
        public Canvas SimulationArea { get; set; }

        public DelegateCommand CreateNewProjectCommand { get; }
        public DelegateCommand AddOpenSystemCommand { get; }
        public DelegateCommand AddPhysicalMediaCommand { get; }
        public DelegateCommand OpenTrafficWindowCommand { get; }
        public DelegateCommand OpenAccessCommand { get; }
        public DelegateCommand OpenSharingWindowCommand { get; }

        public MainWindowVm1(IOpenSystemBuilder openSystemBuilder, IMediaBuilder mediaBuilder, IMediaManager mediaManager)
        {
            _openSystemBuilder = openSystemBuilder;
            _mediaBuilder = mediaBuilder;
            _mediaManager = mediaManager;

            LoadOrCreateNewProject();

            OpenSystemVm = new OpenSystemVm1(_openSystemBuilder);
            PhysicalMediaVm = new PhysicalMediaVm1(_mediaBuilder, _mediaManager, this);

            CreateNewProjectCommand = new DelegateCommand(() => { CreateNewProject(); });
            AddOpenSystemCommand = new DelegateCommand(() => { AddOpenSystem(); });
            AddPhysicalMediaCommand = new DelegateCommand(() => { AddPhysicalMedia(); });
            OpenTrafficWindowCommand = new DelegateCommand(() => { OpenTrafficWindow(); });
            OpenAccessCommand = new DelegateCommand(() => { OpenAccess(); });
            OpenSharingWindowCommand = new DelegateCommand(() => { OpenSharingWindow(); });
        }

        public void CreateNewProject()
        {
            OpenSystemVm = new OpenSystemVm1(_openSystemBuilder);
            PhysicalMediaVm = new PhysicalMediaVm1(_mediaBuilder, _mediaManager, this);
        }

        public void LoadOrCreateNewProject(NetworkProjectModel1 networkProjectModel = null)
        {
            if (networkProjectModel != null)
            {
                OpenSystemVm.OpenSystemModels.Clear();
                OpenSystemVm.OpenSystemModels.AddRange(networkProjectModel.OpenSystemModels);
            }
            else
            {
                NetworkProjectModel = networkProjectModel;
            }
        }

        private void AddOpenSystem()
        {
            OpenSystemVm.AddOpenSystem();
        }

        private void AddPhysicalMedia()
        {
            PhysicalMediaVm.AddPhysicalMedia();
        }

        private void OpenTrafficWindow()
        {
            var trafficWindow = new TrafficWindow
            {
                DataContext = new TrafficWindowVm(this)
            };
            trafficWindow.Show();
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
            _sharingServiceCallback = new SharingServiceCallback(SynchronizationContext.Current, this);
            _sharingServiceClient = new SharingServiceClient(new InstanceContext(_sharingServiceCallback), "ServerEndpoint");
            _sharingServiceClient.Open();

            _sharingServiceClient.ConnectToSharedProject();
        }
    }
}
