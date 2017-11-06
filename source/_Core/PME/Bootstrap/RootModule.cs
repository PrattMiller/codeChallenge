using Autofac;
using PME.Logging;
using PME.Configuration;

namespace PME.Bootstrap
{
    public class RootModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<DateTimeNow>()
                .AsSelf()
                .As<INow>()
                .SingleInstance();

            builder.RegisterType<NoOpLogFactory>()
                .AsSelf()
                .As<ILogFactory>()
                .SingleInstance();

            builder.RegisterType<InMemoryAppSettings>()
                .AsSelf()
                .As<IAppSettings>()
                .SingleInstance();

            builder.RegisterType<RngRandom>()
                .AsSelf() 
                .As<IRandom>()
                .SingleInstance();

            builder.RegisterType<DateTimeClock>()
                .AsSelf()
                .As<IClock>()
                .As<IAcceleratedClock>()
                .InstancePerDependency();

        }

    }
}

