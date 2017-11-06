using System;
using System.Threading.Tasks;
using PME.Logging;
using Autofac;

namespace PME
{
    public abstract class AbstractService : Startable, IService
    {

        private readonly ILog _log;

        protected AbstractService(
            IComponentContext componentContext,
            ILogFactory logFactory
            )
        {
            _log = logFactory.CreateForType(this);
        }


        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

    }
}
