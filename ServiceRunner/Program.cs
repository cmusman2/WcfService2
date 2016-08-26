using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfService2;

namespace ServiceRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            // the Uri for this Web Service.  
            Uri baseAddress = new Uri("http://localhost:8090/My_WebService");

            // Create the ServiceHost.  
            // Service1 host = new Service1(typeof(WebService), baseAddress);
            using (ServiceHost host = new ServiceHost(typeof(Service1), baseAddress))
            {
                // Enable metadata publishing.   
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since  
                // no endpoints are explicitly configured, the runtime will create  
                // one endpoint per base address for each service contract implemented  
                // by the service.  
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.  
                host.Close();
          //      ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
            }
        }
    }
}
