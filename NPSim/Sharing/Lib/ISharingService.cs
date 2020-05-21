using System.ServiceModel;

namespace NPSim.Sharing.Lib
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISharingServiceCallback))]
    public interface ISharingService
    {
        [OperationContract]
        void ConnectToSharedProject();
    }
}
