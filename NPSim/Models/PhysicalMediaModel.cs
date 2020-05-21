using System.Runtime.Serialization;
using NPSim.Entities.PhysicalLayer.Media;

namespace NPSim.Models
{
    [DataContract]
    public class PhysicalMediaModel
    {
        [DataMember]
        public PhysicalMedia PhysicalMedia { get; }

        public PhysicalMediaModel(PhysicalMedia physicalMedia)
        {
            PhysicalMedia = physicalMedia;
        }
    }
}
