using System.ServiceProcess;

namespace PME
{
    public class RunnableServiceBase : ServiceBase
    {
        private readonly IRunnable _instance;

        public RunnableServiceBase(IRunnable instance)
        {
            _instance = instance;
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _instance.Start();
        }

        protected override void OnStop()
        {
            _instance.Stop();
            base.OnStop();
        }

    }
}

