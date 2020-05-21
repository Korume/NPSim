using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NPSim.Entities.PhysicalLayer.Nic.EventHandlers;

namespace NPSim.Entities.PhysicalLayer.Nic
{
    [DataContract]
    [KnownType(typeof(NetworkInterface))]
    public class BaseNetworkInterfaceController : INetworkInterfaceController
    {
        [DataMember]
        private readonly List<INetworkInterface> _networkInterfaces;

        [DataMember]
        public uint NetworkInterfaceCount { get; private set; }
        
        public IReadOnlyList<INetworkInterface> NetworkInterfaces { get => _networkInterfaces; }
        
        public BaseNetworkInterfaceController(List<INetworkInterface> networkInterfaces)
        {
            _networkInterfaces = networkInterfaces;

            foreach (var networkInterface in NetworkInterfaces)
            {
                networkInterface.DataReceived += ProcessDataReceiving;
            }
        }

        public void Send(PhysicalAddress physicalAddress, byte[] data)
        {
            var sender = NetworkInterfaces.FirstOrDefault(n => n.MacAddress == physicalAddress);

            sender.Send(data);
        }

        private void ProcessDataReceiving(object sender, DataReceivedEventArgs e)
        {
            var networkInterface = (INetworkInterface)sender;
            var receivedData = networkInterface.DequeueData();
            if (receivedData != null)
            {
                System.Diagnostics.Debug.WriteLine(System.Text.Encoding.ASCII.GetString(receivedData));
                //Process()
            }
        }
    }
}
