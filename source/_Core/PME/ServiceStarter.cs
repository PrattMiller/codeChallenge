using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Net;
using System.Runtime.InteropServices;
using Autofac;
using System.Threading;
using PME.Configuration;
using PME.Logging;
using PME.Bootstrap;
using System.Collections.Generic;
using System.Linq;

namespace PME
{
    public static class ServiceStarter
    {
        private static IContainer _container;
        private static IAppSettings _appSettings;

        private static ConsoleEventDelegate _consoleCancelHandler;
        private static bool _cancel;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        private delegate bool ConsoleEventDelegate(int eventType);

        public static void Start<T>(ServiceStarterParams startParameters) where T : class, IService
        {
            if (startParameters.Name.IsMissing())
            {
                throw new InvalidOperationException("Missing required application name");
            }

            var args = Environment.GetCommandLineArgs();

            bool runInConsole = Debugger.IsAttached || Array.Exists(args, x => x.Equals("/console", StringComparison.OrdinalIgnoreCase));
            bool runDebugger = Array.Exists(args, x => x.Equals("/debug", StringComparison.OrdinalIgnoreCase));
            bool noLog = Array.Exists(args, x => x.Equals("/nolog", StringComparison.OrdinalIgnoreCase));

            // Add a handler for any unhandled exceptions.
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += AppDomain_UnhandledException;

            if (runDebugger)
            {
                Debugger.Launch();
            }


            T instance = default(T);

            try
            {
                // Allow any type of client-side certificate to engage in TLS communications
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterModule<RootModule>();

                foreach (var kernelModule in startParameters.Modules)
                {
                    // Skip the root module since it was already loaded manually
                    if (kernelModule is RootModule)
                    {
                        continue;
                    }
                    containerBuilder.RegisterModule(kernelModule);
                }

                // Register the service that is being started...
                containerBuilder.RegisterType<T>()
                    .AsSelf()
                    .SingleInstance();

                // If passing a /nolog parameter, then override with a NoOp implementation
                if (noLog)
                {
                    containerBuilder.RegisterType<NoOpLog>()
                        .As<ILog>()
                        .SingleInstance();
                }


                _container = containerBuilder.Build();

                _appSettings = _container.Resolve<IAppSettings>();
                _appSettings.Start();

                try
                {
                    instance = _container.Resolve<T>();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to instantiate the service component - " + ex.Message, ex);
                }
                
                if (runInConsole)
                {
                    // Register a delegate to handle the Ctrl+C event
                    _consoleCancelHandler = new ConsoleEventDelegate(ConsoleEventCallback);
                    SetConsoleCtrlHandler(_consoleCancelHandler, true);

                    Console.Title = instance.GetType().Name;
                    instance.Start();

                    while (true)
                    {
                        if (_cancel)
                        {
                            break;
                        }

                        Thread.Sleep(250);
                    }
                    
                    instance.Stop();
                }
                else
                {
                    var runnableServiceBase = new RunnableServiceBase(instance);
                    ServiceBase.Run(runnableServiceBase);
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(
                    ex.Message,
                    ex.RenderDetailString()
                    );

                if (Environment.UserInteractive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "Fatal exception occurred: {0}",
                        ex.RenderDetailString()
                        );
                    Console.ResetColor();
                }

                if (!ReferenceEquals(instance, null))
                {
                    if (instance.IsRunning)
                    {
                        instance.Stop();
                    }
                }
            }

            if (!ReferenceEquals(_container, null))
            {
                _container.Dispose();
            }

            // NOTE: ProcessExit.Exit() will allow subscribing background threads to continue, but
            // give just a smigen of time to allow for them to complete without the framework trying
            // to close them
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }

        private static bool ConsoleEventCallback(int eventType)
        {
            _cancel = true;
            return true;
        }

        private static void AppDomain_UnhandledException(object exception, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
            {
                Environment.Exit(1);
            }
        }
    }
    public class ServiceStarterParams
    {
        public Module[] Modules { get; private set; }
        
        public string Name { get; private set; }

        public class Builder
        {
            private List<Module> _modules = new List<Module>();
            private string _name;

            public Builder(string name)
            {
                _name = name;
            }

            public Builder WithModule(Module module)
            {
                if (_modules.Where(m => m.GetType() == module.GetType()).Any())
                {
                    throw new InvalidOperationException("Can not register a module of the same type twice");
                }
                _modules.Add(module);

                return this;
            }

            public Builder WithModule<T>() where T : Module, new()
            {
                WithModule(new T());
                return this;
            }

            public ServiceStarterParams Build()
            {
                return new ServiceStarterParams()
                {
                    Modules = _modules.ToArray(),
                    Name = _name,
                };
            }
        }
    }
}

