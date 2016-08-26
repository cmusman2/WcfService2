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


namespace WorkingServiceContainer
{
    public partial class ServiceContainer : ServiceBase
    {
        ServiceHost serviceHost = null;
        public ServiceContainer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
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
                serviceHost = new ServiceHost(typeof(WcfService2.Service1), baseAddress);


                serviceHost.AddServiceEndpoint(typeof(WcfService2.IService1), new WSHttpBinding(), "");

                    // Enable metadata publishing.   
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                serviceHost.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since  
                // no endpoints are explicitly configured, the runtime will create  
                // one endpoint per base address for each service contract implemented  
                // by the service.  
                serviceHost.Open();
                    EventLog.WriteEntry("The service is ready at {0}", baseAddress.ToString());
                    // Console.WriteLine("The service is ready at {0}", baseAddress);
                    //Console.WriteLine("Press <Enter> to stop the service.");
                    EventLog.WriteEntry("Ready...");
                    //Console.ReadLine();
                    // Close the ServiceHost.  
                    // host.Close();
                    //      ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
               


                EventLog.WriteEntry("Service1 service started.");
            }catch(Exception exp)
            {
                EventLog.WriteEntry("Error occured:"+exp.InnerException);
            }
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
