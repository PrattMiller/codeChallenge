using System;
using System.Reflection;
using System.Web.Http;
using Autofac;

namespace PME.Sample.Service.Bootstrap
{
    public class SampleServiceModule : Autofac.Module
    {

        private static readonly Assembly _assembly = typeof(SampleServiceModule).Assembly;

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(_assembly)
                .Where(x => typeof(ApiController).IsAssignableFrom(x))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();

        }
    }
}

