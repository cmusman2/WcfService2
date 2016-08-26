using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        void AddData(Data s);

        [OperationContract]
        Data[] GetCurrentData();


        [OperationContract]
        [FaultContract(typeof(ExceptionHandelingInWCF))]
        AuthorResponse GetInfo(AutherRequest Req);


        [OperationContract]
        [FaultContract(typeof(ExceptionHandelingInWCF))]
        Task<List<hotelsummary>> GetHotels(HotelSearchRequest Req);


        // TODO: Add your service operations here
    }


    [MessageContract]
    public class AutherRequest
    {
        [MessageHeader]
        public string Username;
        public string Password;
    }





    [MessageContract]
    public class AuthorResponse
    {
        [MessageBodyMember]
        public Auther auhther;
    }


    [DataContract]
    public class ExceptionHandelingInWCF
    {
        public ExceptionHandelingInWCF()
        { }
        public ExceptionHandelingInWCF(string errorMessage, string ErrorDescription)
        {
            this.Message = errorMessage;
            this.Description = ErrorDescription;
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
 


[DataContract]
    public class Auther
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Artical { get; set; }
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // DataContracts for the Web Service  
    [DataContract]
    public class Data
    {
        private string text;

        [DataMember]
        public string DataLabel
        {
            set { this.text = value; }
            get { return this.text; }
        }
    }


    public class xDataSource
    {
        // Get the current users MyDocuments folder  
        static string directory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        // Our File Name. You can change it in your project.  
        static string fileName = "/myServiceFile.txt";




        // static variable  
        // Will contain the content, for accessing and saving the data  
        static List<string> currentData;

        // This method saves the Data sent  
        public void Save(string dataLabel)
        {
            currentData.Add(dataLabel);
            Directory.CreateDirectory(directory);
            File.Create(directory + fileName).Close();

            File.WriteAllLines(directory + fileName, currentData);
        }

        // Gets the current list.  
        public List<string> GetDataList() 
        {
            return currentData;
        }

        public xDataSource()
        {
            try
            {
                if (!(File.Exists(directory + fileName)))
                {
                    File.Create(directory + fileName).Close();
                }
            }finally
            {
                //
            }
            currentData = File.ReadAllLines(directory + fileName).ToList();
        }
    }
}