using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NPSim.Models;
using NPSim.Sharing.Lib;
using NPSim.ViewModels;

namespace NPSim.Sharing.Client
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class SharingServiceCallback : ISharingServiceCallback
    {
        private SynchronizationContext _uiSyncContext;
        private MainWindowVm1 _mainWindowVm;

        public SharingServiceCallback(SynchronizationContext synchronizationContext, MainWindowVm1 mainWindowVm)
        {
            _uiSyncContext = synchronizationContext;
            _mainWindowVm = mainWindowVm;
        }

        public Task LoadProjectAsync(NetworkProjectModel1 networkProjectModel)
        {
            return Task.Factory.StartNew(() =>
            {
                _uiSyncContext.Post((c) => _mainWindowVm.LoadOrCreateNewProject((NetworkProjectModel1)c), networkProjectModel);
            });
        }
    }
}
