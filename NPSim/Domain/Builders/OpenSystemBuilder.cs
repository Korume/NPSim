using NPSim.Entities;

namespace NPSim.Domain.Builders
{
    public class OpenSystemBuilder : IOpenSystemBuilder
    {
        private uint _computerCount = 0;

        private readonly INetworkInterfaceControllerBuilder _nicBuilder;

        public OpenSystemBuilder(INetworkInterfaceControllerBuilder nicBuilder)
        {
            _nicBuilder = nicBuilder;
        }

        public Computer BuildComputer()
        {
            var nic = _nicBuilder.BuildNetworkInterfaceController();

            var computer = new Computer($"PC{_computerCount}", 2);
            computer.InstallNic(nic);

            _computerCount++;

            return computer;
        }
    }
}
