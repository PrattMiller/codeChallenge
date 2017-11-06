using System;
using System.Net;
using System.Web.Http;
using Autofac;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.Owin.Hosting;
using Owin;
using PME.Configuration;
using PME.Logging;
using PME.Web;

namespace PME.Sample.Service
{
    internal class SampleService : AbstractService
    {

        private IDisposable _webHost;
        
        private readonly IComponentContext _componentContext;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IAppSettings _appSettings;
        private readonly ILog _log;

        public SampleService(
            IComponentContext componentContext,
            ILifetimeScope lifetimeScope,
            IAppSettings appSettings,
            ILogFactory logFactory
            ) : base(componentContext, logFactory)
        {
            _componentContext = componentContext;
            _lifetimeScope = lifetimeScope;
            _appSettings = appSettings;

            _log = logFactory.CreateForType(this);
        }


        protected override void OnStart()
        {
            base.OnStart();
        
            // NOTE: It is possible that this method is called several times.  If the web host
            // component is available, do not attempt to create another one...
            if (!ReferenceEquals(_webHost, null))
            {
                return;
            }

            var port = (int) Convert.ChangeType(_appSettings.Get("Http.HostPort"), typeof(int));

            var hostingUri = string.Format("https://+:{0}/", port);

            var startOptions = new StartOptions
            {
                Urls = { hostingUri },
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };

            try
            {
                _webHost = WebApp.Start(startOptions, WebAppConfig);

                foreach (var url in startOptions.Urls)
                {
                    _log.Info(string.Format("Listening: {0}", url));
                }
            }
            catch (Exception ex)
            {
                _log.ErrorFormat(ex, "Unable to start web listener - {0}", ex.Message);

                throw;
            }
        }

        private void WebAppConfig(IAppBuilder appBuilder)
        {
            var webListener = appBuilder.Properties["Microsoft.Owin.Host.HttpListener.OwinHttpListener"] as OwinHttpListener;

            if (ReferenceEquals(webListener, null))
            {
                throw new Exception("Invalid service configuration - missing OWIN http listener");
            }

            webListener.Listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            webListener.Listener.IgnoreWriteExceptions = true;

            //appBuilder.SetLoggerFactory(_owinLogFactory);

            var config = new HttpConfiguration
            {
                DependencyResolver = new HttpDependencyResolver(_lifetimeScope),
            };

            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
        }
        
        protected override void OnStop()
        {
            if (!ReferenceEquals(_webHost, null))
            {
                _webHost.Dispose();
            }

        }

    }
}


