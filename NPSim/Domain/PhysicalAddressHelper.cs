using System;
using System.Net.NetworkInformation;

namespace NPSim.Domain
{
    public static class PhysicalAddressHelper
    {
        public static readonly PhysicalAddress None = PhysicalAddress.None;
        public static readonly PhysicalAddress Broadcast = new PhysicalAddress(new byte[6] { 255, 255, 255, 255, 255, 255 });

        public static PhysicalAddress GeneratePhysicalAddress()
        {
            var random = new Random();
            var octets = new byte[6];
            random.NextBytes(octets);

            return new PhysicalAddress(octets);
        }
    }
}
