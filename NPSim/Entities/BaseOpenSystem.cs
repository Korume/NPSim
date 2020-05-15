using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Entities
{
    public abstract class BaseOpenSystem : IOpenSystem
    {
        public bool IsActive { get; private set; }
        public string Name { get; private set; }
        public uint NicCount { get; }
        public List<INetworkInterfaceController> NicCollection { get; }

        public BaseOpenSystem(string name, uint nicCount)
        {
            Name = name;
            NicCount = nicCount;
            NicCollection = new List<INetworkInterfaceController>();
        }

        public void InstallNic(INetworkInterfaceController nic)
        {
            if (NicCollection.Contains(nic))
            {
                return;
            }

            NicCollection.Add(nic);
        }

        public void UninstallNic(INetworkInterfaceController nic)
        {
            NicCollection.Remove(nic);
        }

        public void SendData(PhysicalAddress sourceAddress, byte[] data)
        {
            var nic = NicCollection.FirstOrDefault(c => c.NetworkInterfaces.Any(n => n.MacAddress == sourceAddress));

            nic.Send(sourceAddress, data);
        }

        public IEnumerable<INetworkInterface> GetAvailableNetworkInterfaces()
        {
            if (NicCollection.Any())
            {
                return NicCollection.SelectMany(c => c.NetworkInterfaces.Where(n => n.AttachedMedia == null));
            }

            return Enumerable.Empty<INetworkInterface>();
        }
    }
}
