using NPSim.Entities.PhysicalLayer.Nic;

namespace NPSim.Entities.PhysicalLayer.Media
{
    public class Connector
    {
        public bool IsAttached { get => ConnectionEndpoint != null; }
        public IConnectionEndpoint ConnectionEndpoint { get; private set; }

        public void Attach(IConnectionEndpoint connectionEndpoint)
        {
            ConnectionEndpoint = connectionEndpoint;
        }

        public void Detach()
        {
            ConnectionEndpoint = null;
        }
    }
}
