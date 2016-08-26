using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WcfService2
{
    public class DataSource
    {


        public async static Task<List<hotelsummary>> GetData(String city, DateTime sdate, DateTime edate)
        {


            using (var client = new System.Net.Http.HttpClient())
            {

                try
                {

                    String x = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?><ns2:hotellistresponse xmlns:ns2=""http://v3.hotel.wsapi.ean.com""><customersessionid>0ABAAA42-487E-F915-62D2-4C6B0D9015B0</customersessionid><numberofroomsrequested>0</numberofroomsrequested><moreresultsavailable>false</moreresultsavailable><hotellist size=""1374"" activepropertycount=""1374""><hotelsummary order=""0""><hotelid>284677</hotelid><name>Best Western Plus Delmere Hotel</name><address1>130 Sussex Gardens</address1><city>London</city><postalcode>W2 1UB</postalcode><countrycode>GB</countrycode><airportcode>LHR</airportcode><propertycategory>1</propertycategory><hotelrating>3.0</hotelrating><confidencerating>94</confidencerating><amenitymask>0</amenitymask><tripadvisorrating>4.0</tripadvisorrating><tripadvisorreviewcount>139</tripadvisorreviewcount><tripadvisorratingurl>http://www.tripadvisor.com/img/cdsi/img2/ratings/traveler/4.0-16366-4.gif</tripadvisorratingurl><locationdescription>Near Marble Arch</locationdescription><shortdescription>&amp;lt;p&amp;gt;&amp;lt;b&amp;gt;Property Location&amp;lt;/b&amp;gt; &amp;lt;br /&amp;gt;With a stay at Best Western Plus Delmere Hotel in London (Paddington), you&amp;apos;ll be convenient to Hyde Park Speakers&amp;apos; Corner and Selfridges.  This hotel is close to</shortdescription><highrate>284.29</highrate><lowrate>163.91</lowrate><ratecurrencycode>USD</ratecurrencycode><latitude>51.51563</latitude><longitude>-0.17226</longitude><proximitydistance>3.075692</proximitydistance><proximityunit>MI</proximityunit><hotelindestination>true</hotelindestination><thumbnailurl>/hotels/1000000/30000/21700/21632/21632_126_t.jpg</thumbnailurl><deeplink>http://www.travelnow.com/templates/396041/hotels/284677/overview?lang=en&amp;amp;currency=USD&amp;amp;standardCheckin=null/null/null&amp;amp;standardCheckout=6/7/2018</deeplink></hotelsummary></hotellist></ns2:hotellistresponse>";
                    String fp = @"C:\net\TravelSys2\TravelSys\App_Data\appdata.xml";
                    if (File.Exists(fp))
                    {
                        x = File.ReadAllText(fp);
                        if (!String.IsNullOrEmpty(x)) { if (x.IndexOf("<?xml") == -1) x = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?>" + x; }
                    }


                    x = await GetData2(city, sdate, edate);

                    XmlReaderSettings settings = new XmlReaderSettings();
                    StringReader textReader = new StringReader(x);
                    XmlReader xml = XmlReader.Create(textReader, settings);



                    XmlSerializer xser = new XmlSerializer(typeof(HotelList), "http://v3.hotel.wsapi.ean.com/");
                    HotelList hl = (HotelList)xser.Deserialize(xml);
                    Console.WriteLine("Sucess");


                    int counts = 0;

                    List<hotelsummary> hls = new List<hotelsummary>();

                    foreach (var h in hl.Hotels.Hotels)
                    {
                        hls.Add(h);
                        counts++;
                        Console.WriteLine(h.name);
                        if (counts > 200) break;
                    }

                    return hls;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
            }
            return null;

        }
         

        public static Task<String> GetData2(String city, DateTime sdate, DateTime edate)
        {


            using (var client = new System.Net.Http.HttpClient())
            {

                try
                {
                    client.BaseAddress = new Uri("http://www.lowestroomrates.com");
                    var query = new System.Net.Http.FormUrlEncodedContent(new[]
              {
                  new KeyValuePair<string, string>("sign", "tak12e"),
                  new KeyValuePair<string, string>("pin", "vS0GKOzRwyZBSJX8gO2bHH4XVYhv-bNi2Eyuf4ZhvHs1uXmTq"),

                 });

                    var result = client.PostAsync("/src/authenticate.php", query).Result;
                    string resultContent = result.Content.ReadAsStringAsync().Result;

                    String sd = sdate.ToString("dd/MM/yy");
                    String ed = edate.ToString("dd/MM/yy");




                    query = new System.Net.Http.FormUrlEncodedContent(new[]
              {
                  new KeyValuePair<string, string>("xuid", resultContent),
                  new KeyValuePair<string, string>("yzid0x", resultContent),
                  new KeyValuePair<string, string>("CityAjaxH", city),
                  new KeyValuePair<string, string>("SDATEH", sd),
                  new KeyValuePair<string, string>("EDATEH", ed),
                 // new KeyValuePair<string, string>("country", "gb"),
                  new KeyValuePair<string, string>("maxnum", "5"),

              });

                    result = client.PostAsync("/src/htllist.php", query).Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    return Task.FromResult(resultContent);
                }
                catch (Exception exp)
                {
                    Console.Out.WriteLine("Error:" + exp.Message);
                }

                finally
                {

                }

                return null;
            }

        }
    }

    public class SearchForm
    {

        [Required]
        [Display(Name = "From Month")]
        public String FromMonth { get; set; }

        [Required]
        [Display(Name = "From Day")]
        public String FromDay { get; set; }

        [Required]
        [Display(Name = "To Month")]
        public String ToMonth { get; set; }

        [Required]
        [Display(Name = "To Day")]
        public String ToDay { get; set; }


        public String FromDate { get; set; }

        [DateGreater("FromDate")]
        public String ToDate { get; set; }


        [Required]
        [Display(Name = "City")]
        public String City { get; set; }

        [Display(Name = "Currency")]
        public String Currency { get; set; }

    }


    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterAttribute : ValidationAttribute
    {
        public DateGreaterAttribute(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            SearchForm frm = (SearchForm)validationContext.ObjectInstance;

            int d, m, y = 0;
            d = Int32.Parse(frm.FromDay);
            m = Int32.Parse(frm.FromMonth.Substring(0, 2));
            y = Int32.Parse(frm.FromMonth.Substring(2, 2));

            DateTime fromDate = new DateTime(y, m, d);

            d = Int32.Parse(frm.ToDay);
            m = Int32.Parse(frm.ToMonth.Substring(0, 2));
            y = Int32.Parse(frm.ToMonth.Substring(2, 2));

            DateTime toDate = new DateTime(y, m, d);


            if ((toDate - fromDate).TotalDays > 28)
            {
                return new ValidationResult("Maximum 28 days allowed");
            }
            else
            if (toDate > fromDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Check out date should be later than check in date");
            }

        }
    }


    [XmlRoot("hotellistresponse", Namespace = "http://v3.hotel.wsapi.ean.com/")]
    public class HotelList
    {


        [XmlElement(ElementName = "hotellist", Namespace = "")]
        public summryList Hotels { get; set; }

        [XmlElement(Namespace = "")]
        public String customersessionid { get; set; }
        [XmlElement(Namespace = "")]
        public String numberofroomsrequested { get; set; }
        [XmlElement(Namespace = "")]
        public String moreresultsavailable { get; set; }
    }


    public class summryList
    {
        [XmlElement(ElementName = "hotelsummary", Namespace = "")]
        public List<hotelsummary> Hotels { get; set; }
    }

    public class hotelsummary
    {
        private String Rate;
        private String ShortDescription;

        private String thumbnailUrl;
        [XmlElement(Namespace = "")]
        public String hotelid { get; set; }

        [XmlElement(Namespace = "")]
        public String name { get; set; }

        [XmlElement(Namespace = "")]
        public String address1 { get; set; }

        [XmlElement(Namespace = "")]
        public String city { get; set; }

        [XmlElement(Namespace = "")]
        public String postalcode { get; set; }

        [XmlElement(Namespace = "")]
        public String countrycode { get; set; }

        [XmlElement(Namespace = "")]
        public String tripadvisorratingurl { get; set; }

        [XmlElement(Namespace = "")]
        public String thumbnailurl { get { return thumbnailUrl.Replace("_t.", "_l."); } set { thumbnailUrl = "http://images.travelnow.com" + value; } }

        [XmlElement(Namespace = "")]
        public String ratecurrencycode { get; set; }

        [XmlElement(Namespace = "")]
        public String locationdescription { get; set; }

        [XmlElement(Namespace = "")]
        public String shortdescription { get { return ShortDescription.Substring(ShortDescription.IndexOf("br /&gt;") + 8); } set { ShortDescription = value; } }

        [DisplayFormat(DataFormatString = "{0:#.#}")]
        [XmlElement(Namespace = "")]
        public String lowrate { get { return ratecurrencycode + " " + Rate; } set { Rate = value; } }

    }
}