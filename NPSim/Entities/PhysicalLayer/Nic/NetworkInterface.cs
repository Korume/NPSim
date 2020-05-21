using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Threading;
using NPSim.Entities.PhysicalLayer.Media;
using NPSim.Entities.PhysicalLayer.Nic.EventHandlers;

namespace NPSim.Entities.PhysicalLayer.Nic
{
    [DataContract]
    public class NetworkInterface : INetworkInterface
    {
        [DataMember]
        private readonly byte[] _macAddressBytes;
        private readonly PhysicalAddress _macAddress;
        
        public PhysicalAddress MacAddress { get => _macAddress ?? new PhysicalAddress(_macAddressBytes); }

        [DataMember]
        public bool IsActivated { get; private set; }

        [DataMember]
        public IMedia AttachedMedia { get; private set; }

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        private readonly Queue<byte[]> QueuedData = new Queue<byte[]>();

        public NetworkInterface(PhysicalAddress macAddress)
        {
            _macAddress = macAddress;
            _macAddressBytes = MacAddress.GetAddressBytes();
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
