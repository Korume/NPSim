using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Entities.PhysicalLayer.Media
{
    public interface IMedia
    {
        void AttachTo(IConnectionEndpoint endpoint);
        void DetachFrom(IConnectionEndpoint endpoint);
        void Transmit(INetworkInterface networkInterface, byte[] data);
    }
}