using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using WcfService2;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        private const string AutherId = "test"; 

        // Adds the Body to the methods  
        public void AddData(Data d)
        {
            xDataSource ds = new xDataSource();
            ds.Save(d.DataLabel);
        }

        public Data[] GetCurrentData()
        {
            xDataSource sd = new xDataSource();
            List<string> dataList = sd.GetDataList();
            Data[] dataArray = new Data[dataList.Count];
            for (int i = 0; i < dataList.Count; i++)
            {
                Data s = new Data();
                s.DataLabel = dataList[i];
                dataArray[i] = s;
            }
            return dataArray;
        }

        public async Task<List<hotelsummary>> GetHotels(HotelSearchRequest Req)
        {
             List <hotelsummary> list= await DataSource.GetData(Req.destination, Req.date, Req.date.AddDays(Req.nights));
            return list;
        }

        public AuthorResponse GetInfo(AutherRequest Req)
        {
            //AppDBContext db = new AppDBContext();
            //var users = db.Users.Where(u => (u.username == Req.Username) || (u.password == Req.Password));//odd

            List<User> dbusers = new List<User>();
            dbusers.Add(new User { username = "test user" });
            dbusers.Add(new User { username = "best user" });
            var users = dbusers.Where(u=> (u.username==Req.Username) ||(u.password==Req.Password));//odd


            
            
             

            //if (Req.AutherId != AutherId)
            if (!(dbusers.Count()>0))
            {
                //throw new FaultException<string>(x, new FaultReason("User provided invalid credentials"));
                
                ExceptionHandelingInWCF except = new ExceptionHandelingInWCF();
                except.Message = "Business Rule violatuion";
                except.Description = "Invalid id";
                throw new FaultException<ExceptionHandelingInWCF>(new ExceptionHandelingInWCF(except.Message, except.Description),new FaultReason("Invalid id provided"));
            }



            AuthorResponse r = new AuthorResponse();

            // create an object of Auther Class
            r.auhther = new Auther();
            r.auhther.FirstName = Req.Username;
            r.auhther.LastName = "Well";
            r.auhther.Artical = "Do work";
            //r.S.Artical = "Learn WCF";

            return r;

        }
    }
}
