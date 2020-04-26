using System;
using System.Net.NetworkInformation;
using NPSim.Entities.PhysicalLayer.Media;
using NPSim.Entities.PhysicalLayer.Nic.EventHandlers;

namespace NPSim.Entities.PhysicalLayer.Nic
{
    public interface INetworkInterface : IConnectionEndpoint
    {
        PhysicalAddress MacAddress { get; }
        bool IsActivated { get; }
        IMedia AttachedMedia { get; }

        event EventHandler<DataReceivedEventArgs> DataReceived;

        void Activate();
        void Deactivate();
        void Send(byte[] data);
        void EnqueueData(byte[] data);
        byte[] DequeueData();
    }
}