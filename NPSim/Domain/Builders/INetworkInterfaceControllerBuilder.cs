using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Domain.Builders
{
    public interface INetworkInterfaceControllerBuilder
    {
        BaseNetworkInterfaceController BuildNetworkInterfaceController();
    }
}