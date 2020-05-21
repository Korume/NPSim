using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPSim.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace NPSim.ViewModels
{
    public class TrafficWindowVm : BindableBase
    {
        public IEnumerable<OpenSystemModel1> OpenSystemModels { get; }

        public OpenSystemModel1 OpenSystemSender { get; set; }
        public OpenSystemModel1 OpenSystemReceiver { get; set; }

        public DelegateCommand CreateTrafficCommand { get; }

        public TrafficWindowVm(MainWindowVm1 mainWindowVm)
        {
            OpenSystemModels = mainWindowVm.OpenSystemVm.OpenSystemModels;

            CreateTrafficCommand = new DelegateCommand(() => { CreateTraffic(); });
        }

        //TODO: Add sender/receiver and media connection null validation
        private void CreateTraffic()
        {
            var computer1 = OpenSystemSender.OpenSystem;
            var computer2 = OpenSystemReceiver.OpenSystem;

            var data = Encoding.ASCII.GetBytes("Hello Network!");

            //TODO: if computer1 doesn't have connection with other devices

            var address = computer1.NicCollection.First().NetworkInterfaces.First().MacAddress;

            computer1.SendData(address, data);
        }

    }
}
