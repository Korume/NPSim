using System.Runtime.Serialization;
using NPSim.Entities;

namespace NPSim.Models
{
    [DataContract]
    public class OpenSystemModel
    {
        [DataMember]
        public BaseOpenSystem OpenSystem { get; }

        public OpenSystemModel(BaseOpenSystem openSystem)
        {
            OpenSystem = openSystem;
        }
    }
}
