using System.ServiceModel;
using System.Threading.Tasks;
using NPSim.Models;

namespace NPSim.Sharing.Lib
{
    public interface ISharingServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        Task LoadProjectAsync(NetworkProjectModel1 networkProjectModel);
    }
}
