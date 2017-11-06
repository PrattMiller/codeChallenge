using Autofac;

namespace PME.Test.Boostrap
{
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<TestClock>()
                .AsSelf()
                .As<IClock>()
                .As<INow>()
                .SingleInstance();
            
        }
    }
}

