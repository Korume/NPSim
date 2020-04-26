using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NPSim.Entities.PhysicalLayer.Nic
{
    public interface INetworkInterfaceController
    {
        IReadOnlyList<INetworkInterface> NetworkInterfaces { get; }

        void Send(PhysicalAddress physicalAddress, byte[] data);
    }
}
