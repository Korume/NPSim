using NPSim.Entities.PhysicalLayer.Media;

namespace NPSim.Models
{
    public class PhysicalMediaModel
    {
        public PhysicalMedia PhysicalMedia { get; }

        public PhysicalMediaModel(PhysicalMedia physicalMedia)
        {
            PhysicalMedia = physicalMedia;
        }
    }
}
