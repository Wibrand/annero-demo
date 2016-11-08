using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Azure.WebJobs.Host;

namespace webjob_admin_worker
{
    class Program
    {
        static void Main()
        {
            var storageConnection = MyConfigurationManager.GetConnectionString("AzureWebJobsStorage", MyConfigurationManager.ConnectionType.Custom);
            Console.WriteLine($"**** {storageConnection}");
            JobHostConfiguration config = new JobHostConfiguration(storageConnection);
            var servicesBusConnectionString = removeEntityPathFromServiceBusConnectionString(MyConfigurationManager.GetConnectionString("ServiceBusAdminConnectionString", MyConfigurationManager.ConnectionType.Custom));


            Console.WriteLine($"**** {servicesBusConnectionString}");
            ServiceBusConfiguration serviceBusConfig = new ServiceBusConfiguration
            {
                ConnectionString = servicesBusConnectionString
            };
            config.UseServiceBus(serviceBusConfig);

            var host = new JobHost(config);
            host.RunAndBlock();
        }
         
        private static string removeEntityPathFromServiceBusConnectionString(string serviceBusConnectionString)
        {
            if (serviceBusConnectionString.IndexOf("EntityPath") > -1)
            {
                StringBuilder sb = new StringBuilder();
                var connectionStringSplit = serviceBusConnectionString.Split(';');
                foreach (var s in connectionStringSplit)
                {
                    if(s.IndexOf("EntityPath") < 0)
                    {
                        sb.Append(s + ";");
                    }
                }

                return sb.ToString().TrimEnd(';');
             }
            else
            {
                return serviceBusConnectionString;
            }

        }
    }
}
