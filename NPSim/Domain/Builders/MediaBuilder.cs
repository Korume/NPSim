using NPSim.Entities.PhysicalLayer;
using NPSim.Entities.PhysicalLayer.Media;

namespace NPSim.Domain.Builders
{
    public class MediaBuilder : IMediaBuilder
    {
        public PhysicalMedia BuildPhysicalMedia()
        {
            var physicalMedia = new PhysicalMedia(new Cabel(), new[] { new Connector(), new Connector() });

            return physicalMedia;
        }
    }
}
