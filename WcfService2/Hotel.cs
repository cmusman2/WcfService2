using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService2
{
    public class Hotel
    {
        public String EanHotelId { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String Address1 { get; set; }
        public String postalCode { get; set; }
        public String LowRate { get; set; }
        public String ImageUrl { get; set; }
    }
}