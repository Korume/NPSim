using System.Collections.Generic;
using System.Net.NetworkInformation;
using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Entities
{
    public interface IOpenSystem
    {
        void InstallNic(INetworkInterfaceController nic);
        void UninstallNic(INetworkInterfaceController nic);
        void SendData(PhysicalAddress physicalAddress, byte[] data);
        IEnumerable<INetworkInterface> GetAvailableNetworkInterfaces();
    }
}