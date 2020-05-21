using System.Runtime.Serialization;

namespace NPSim.Entities
{
    [DataContract]
    public class Computer : BaseOpenSystem
    {
        public Computer(string name, uint nicCount)
            : base(name, nicCount)
        {
        }
    }
}
