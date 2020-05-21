using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MasterSCADAWindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ServiceBase.Run(new WindowsService());
                return;
            }

            using (ServiceHost host = new ServiceHost(typeof(MasterSCADAWindowsService.DataService)))
            {

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("Master SCADA service host is running");
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
