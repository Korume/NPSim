using System.ServiceModel;
using System.ServiceModel.Channels;
using NPSim.Sharing.Lib;

namespace NPSim.Sharing.Client
{
    public class SharingServiceClient : DuplexClientBase<ISharingService>, ISharingService
    {

        public SharingServiceClient(InstanceContext callbackInstance) :
                base(callbackInstance)
        {
        }

        public SharingServiceClient(InstanceContext callbackInstance, string endpointConfigurationName) :
                base(callbackInstance, endpointConfigurationName)
        {
        }

        public SharingServiceClient(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public SharingServiceClient(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public SharingServiceClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) :
                base(callbackInstance, binding, remoteAddress)
        {
        }

        public void ConnectToSharedProject()
        {
            Channel.ConnectToSharedProject();
        }
    }
}
