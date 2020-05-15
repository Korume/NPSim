using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;

namespace NPSim.ViewModels
{
    public class TrafficWindowVm : BindableBase
    {
        public IEnumerable<OpenSystemVm> OpenSystemVms { get; }

        public OpenSystemVm OpenSystemVmSender { get; set; }
        public OpenSystemVm OpenSystemVmReceiver { get; set; }

        public DelegateCommand CreateTrafficCommand { get; }

        public TrafficWindowVm(MainWindowVm mainWindowVm)
        {
            OpenSystemVms = mainWindowVm.OpenSystemVms;

            CreateTrafficCommand = new DelegateCommand(() => { CreateTraffic(); });
        }

        //TODO: Add sender/receiver and media connection null validation
        private void CreateTraffic()
        {
            var computer1 = OpenSystemVmSender.OpenSystemModel.OpenSystem;
            var computer2 = OpenSystemVmReceiver.OpenSystemModel.OpenSystem;

            var data = Encoding.ASCII.GetBytes("Hello Network!");

            var address = computer1.NicCollection.First().NetworkInterfaces.First().MacAddress;

            computer1.SendData(address, data);
        }

    }
}
