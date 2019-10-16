using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStartStopService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            Console.WriteLine("Sudo Service Starting.");
            Thread ServiceThread = new Thread(new ThreadStart(ServiceRun));
            ServiceThread.Start();
            Console.ReadKey();
            Console.WriteLine("Sudo Service Started.\nPress any key to end!");
            Console.ReadKey();
            Console.WriteLine("Sudo Service Ending.");
            System.Windows.Forms.Application.Exit();
            Console.WriteLine("Sudo Service Ended.\nPress any key to end console and close out!");
            Console.ReadKey();
#else
            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new ArcadeGameStartAndStopService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }

#if DEBUG
        static void ServiceRun()
        {
            ArcadeGameStartAndStopService Service = new ArcadeGameStartAndStopService();
            Service.OnDebugStart();
            System.Windows.Forms.Application.Run();
            Service.OnDebugEnd();
        }
#endif
    }
}
