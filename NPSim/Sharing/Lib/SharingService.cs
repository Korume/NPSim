using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using NPSim.Models;
using NPSim.ViewModels;

namespace NPSim.Sharing.Lib
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults =true)]
    public class SharingService : ISharingService
    {
        private static List<ISharingServiceCallback> _callbackList = new List<ISharingServiceCallback>();
        private MainWindowVm1 _mainWindowVm;

        public SharingService()
        {
            _mainWindowVm = (MainWindowVm1)Application.Current.MainWindow.DataContext;
        }

        public void ConnectToSharedProject()
        {
            var registeredUser = OperationContext.Current.GetCallbackChannel<ISharingServiceCallback>();

            if (!_callbackList.Contains(registeredUser))
            {
                _callbackList.Add(registeredUser);
            }

            var networkProject = new NetworkProjectModel1(_mainWindowVm.OpenSystemVm.OpenSystemModels);

            registeredUser.LoadProjectAsync(networkProject);
        }
    }
}
