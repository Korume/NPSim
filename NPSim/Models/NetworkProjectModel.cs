using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using NPSim.ViewModels;

namespace NPSim.Models
{
    [DataContract]
    public class NetworkProjectModel
    {
        [DataMember]
        public ObservableCollection<OpenSystemVm> OpenSystemVms { get; set; }

        [DataMember]
        public ObservableCollection<PhysicalMediaVm> PhysicalMediaVms { get; set; }

        public NetworkProjectModel()
        {
            OpenSystemVms = new ObservableCollection<OpenSystemVm>();
            PhysicalMediaVms = new ObservableCollection<PhysicalMediaVm>();
        }
    }
}
