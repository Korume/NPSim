using NPSim.Entities.PhysicalLayer.Media;

namespace NPSim.Entities.PhysicalLayer.Nic
{
    public interface IConnectionEndpoint
    {
        void Attach(IMedia media);
        void Detach();
    }
}