using NPSim.Entities.PhysicalLayer.Nic;
using NPSim.Entities.PhysicalLayer.Media;

namespace NPSim.Domain.PhysicalLayer
{
    public interface IMediaManager
    {
        void AttachMediaToConnectionEndpoint(IMedia media, IConnectionEndpoint endpoint);
        void DetachMediaFromConnectionEndpoint(IMedia media, IConnectionEndpoint endpoint);
    }
}