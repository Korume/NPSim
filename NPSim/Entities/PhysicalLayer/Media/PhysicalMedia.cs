using System.Linq;
using System.Runtime.Serialization;
using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Entities.PhysicalLayer.Media
{
    [DataContract]
    public class PhysicalMedia : IMedia
    {
        private const uint _connectorCount = 2;

        public bool HasAvailableConnector { get => _connectors.Any(c => !c.IsAttached); }

        [DataMember]
        private readonly Cabel _cabel;

        [DataMember]
        private readonly Connector[] _connectors;

        public PhysicalMedia(Cabel cabel, Connector[] connectors)
        {
            _cabel = cabel;
            _connectors = connectors;
        }

        public void AttachTo(IConnectionEndpoint endpoint)
        {
            var availableConnector = _connectors.FirstOrDefault(c => !c.IsAttached);
            availableConnector.Attach(endpoint);
        }

        public void DetachFrom(IConnectionEndpoint endpoint)
        {
            var connector = _connectors.SingleOrDefault(c => c.ConnectionEndpoint == endpoint);
            connector.Detach();
        }

        public void Transmit(INetworkInterface sender, byte[] data)
        {
            data = ProcessTransmition(data);
            var dataReceivers = _connectors.Where(c => c.ConnectionEndpoint != sender).Select(c => c.ConnectionEndpoint);
            foreach (INetworkInterface receiver in dataReceivers)
            {
                receiver.EnqueueData(data);
            }
        }

        private byte[] ProcessTransmition(byte[] data)
        {
            return _cabel.ProcessTransmition(data);
        }
    }
}
