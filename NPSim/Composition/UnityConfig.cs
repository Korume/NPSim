using System;
using NPSim.Domain.Builders;
using NPSim.Domain.PhysicalLayer;
using Unity;

namespace NPSim.Composition
{
    public static class UnityConfig
    {
        public static IUnityContainer GetContainer()
        {
            return _container.Value;
        }

        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = Build();
            return container;
        });

        private static IUnityContainer Build()
        {
            var container = new UnityContainer();

            container.RegisterSingleton<IOpenSystemBuilder, OpenSystemBuilder>();
            container.RegisterType<IMediaManager, MediaManager>();
            container.RegisterType<IMediaBuilder, MediaBuilder>();
            container.RegisterType<INetworkInterfaceControllerBuilder, NetworkInterfaceControllerBuilder>();

            return container;
        }
    }
}
