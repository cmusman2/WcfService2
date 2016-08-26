using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace WorkingHost
{
    public partial class Service1 : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public Service1()
        {
            InitializeComponent();
            ServiceName = "Service1";
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Attempting to start Service1.");
            if (serviceHost != null)
            {
                serviceHost.Close();
            }



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
                EventLog.WriteEntry("The service is ready at {0}", baseAddress.ToString());
                // Console.WriteLine("The service is ready at {0}", baseAddress);
                //Console.WriteLine("Press <Enter> to stop the service.");
                EventLog.WriteEntry("Ready");
                Console.ReadLine();
                // Close the ServiceHost.  
                // host.Close();
                //      ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
            }


            EventLog.WriteEntry("Service1 service started.");
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Attempting to stop Service1.");
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
                EventLog.WriteEntry("Service1 service stopped.");
            }
        }
    }
}
