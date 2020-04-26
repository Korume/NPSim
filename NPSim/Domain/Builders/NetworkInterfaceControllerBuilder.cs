using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Domain.Builders
{
    public class NetworkInterfaceControllerBuilder : INetworkInterfaceControllerBuilder
    {
        public BaseNetworkInterfaceController BuildNetworkInterfaceController()
        {
            var macAddress1 = PhysicalAddressHelper.GeneratePhysicalAddress();
            var networkInterface1 = new NetworkInterface(macAddress1);

            var nic = new BaseNetworkInterfaceController(new[] { networkInterface1 });

            return nic;
        }
    }
}
