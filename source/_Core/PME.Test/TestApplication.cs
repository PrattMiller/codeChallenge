using System;
using NUnit.Framework;
using Autofac;
using PME.Bootstrap;
using PME.Test.Boostrap;

namespace PME.Test
{
    internal static class TestApplicationState
    {
        public static IContainer Container { get; set; }
    }

    [SetUpFixture]
    public class TestApplication
    {
        public TestApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<RootModule>();

            builder.RegisterModule<TestModule>();

            TestApplicationState.Container = builder.Build();
        }

        [OneTimeSetUp]
        public void Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<RootModule>();
            builder.RegisterModule<TestModule>();
            
            TestApplicationState.Container = builder.Build();
        }

        [OneTimeTearDown]
        public void Stop()
        {
            TestApplicationState.Container.Dispose();
        }
    }
}

