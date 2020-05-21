using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NPSim.Models
{
    [DataContract]
    public class NetworkProjectModel1
    {
        [DataMember]
        public ObservableCollection<OpenSystemModel1> OpenSystemModels { get; set; }

        public NetworkProjectModel1(ObservableCollection<OpenSystemModel1> openSystemModels)
        {
            OpenSystemModels = openSystemModels;
        }
    }
}
