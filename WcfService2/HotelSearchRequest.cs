using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService2
{
    public class HotelSearchRequest
    {
        public String destination { get; set; }
        public DateTime date { get; set; }
        public int nights { get; set; }
        public Party guests { get; set; }


    }


    public class Party
    {
        public Guest[] adults { get; set; }
        public Guest[] kids { get; set; }

    }


    public class Guest
    {
        public char GuestType { get; set; }
        public int Number { get; set; }
        public int Age { get; set; }
    }

}