using NPSim.Entities;

namespace NPSim.Models
{
    public class OpenSystemModel
    {
        public BaseOpenSystem OpenSystem { get; }

        public OpenSystemModel(BaseOpenSystem openSystem)
        {
            OpenSystem = openSystem;
        }
    }
}
