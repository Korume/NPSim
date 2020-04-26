using NPSim.Entities.PhysicalLayer.Nic;
using NPSim.Entities.PhysicalLayer.Media;

namespace NPSim.Domain.PhysicalLayer
{
    public class MediaManager : IMediaManager
    {
        public void AttachMediaToConnectionEndpoint(IMedia media, IConnectionEndpoint endpoint)
        {
            media.AttachTo(endpoint);
            endpoint.Attach(media);
        }

        public void DetachMediaFromConnectionEndpoint(IMedia media, IConnectionEndpoint endpoint)
        {
            media.DetachFrom(endpoint);
            endpoint.Detach();
        }
    }
}
