using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using NPSim.Entities.PhysicalLayer.Media;
using NPSim.Entities.PhysicalLayer.Nic.EventHandlers;

namespace NPSim.Entities.PhysicalLayer.Nic
{
    public class NetworkInterface : INetworkInterface
    {
        public PhysicalAddress MacAddress { get; }
        public bool IsActivated { get; private set; }
        public IMedia AttachedMedia { get; private set; }

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        private readonly Queue<byte[]> QueuedData = new Queue<byte[]>();

        public NetworkInterface(PhysicalAddress macAddress)
        {
            MacAddress = macAddress;
        }

        public void Attach(IMedia media)
        {
            if (AttachedMedia == null)
            {
                AttachedMedia = media;
            }
        }

        public void Detach()
        {
            if (AttachedMedia != null)
            {
                AttachedMedia = null;
            }
        }

        public void Activate()
        {
            IsActivated = true;
        }

        public void Deactivate()
        {
            IsActivated = false;
        }

        public void Send(byte[] data)
        {
            AttachedMedia.Transmit(this, data);
        }

        public void EnqueueData(byte[] data)
        {
            QueuedData.Enqueue(data);

            var e = new DataReceivedEventArgs();
            OnDataReceived(e);
        }

        public byte[] DequeueData()
        {
            if (QueuedData.Any())
            {
                return QueuedData.Dequeue();
            }

            return null;
        }

        protected void OnDataReceived(DataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }
    }
}
