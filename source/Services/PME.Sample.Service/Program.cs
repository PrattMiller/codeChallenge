using System;
using PME.Bootstrap;
using PME.Web.Bootstrap;

namespace PME.Sample.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var startParams = new ServiceStarterParams.Builder("Sample")
                    .WithModule<RootModule>()
                    .WithModule<WebModule>()
                    .WithModule<SampleServiceModule>();

                ServiceStarter.Start<SampleService>(startParams.Build());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fatal exception:\r\n" + ex.RenderDetailString());
                Console.ResetColor();
            }
        }

    }
}


