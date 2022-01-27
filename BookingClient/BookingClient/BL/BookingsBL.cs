using BookingClient.Models;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Helpers;

namespace BookingClient.BL
{
    public  class BookingsBL
    {
        //private BookingsBL(ILogger<BookingQualityCheck> logger) { }
        private static BookingsBL instance = null;
        public static BookingsBL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingsBL();
                }
                return instance;
            }
        }
    
       
        public List<BookingQualityCheck> GetQualityCheck()
        {
            List<Booking> bookings = getBookingsFromAPI();
            SetDuplicateValue(ref bookings);
            List <BookingQualityCheck> bookingsQualityCheck = bookings.Select(booking => new BookingQualityCheck(booking)).ToList();
            return bookingsQualityCheck;

        }

        private static List<Booking> getBookingsFromAPI()
        {
            try
            {
                string url = "http://localhost:9292/api/bookings";
                //HttpClient client = new HttpClient();
                //string response = await client.GetStringAsync(url);
                //List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(response);
                //return bookings;

                using (WebClient webClient = new System.Net.WebClient())
                {

                    var jsonString = webClient.DownloadString(url);
                    var root = JsonConvert.DeserializeObject<RootBookings>(jsonString);
                    //JObject obj = JObject.Parse(jsonString);
                    //var root = JsonConvert.DeserializeObject<RootBookings>(jsonString);
                    return root.bookings;
                }
            }
            catch (Exception ex)
            {
                //logger.LogError("error in getBookingsList = " + ex.ToString());
                return null;
            }

        }

        private void SetDuplicateValue(ref List<Booking> bookings)
        {
            var query = bookings.GroupBy(b => b.StudentId).Where(s => s.Count() > 1).ToDictionary(x => x.Key, y => y.Count());
            var duplicates = bookings.Where(book => query.ContainsKey(book.StudentId)).ToList().Any(b => b.IsDuplicatStudentPayment = true);
        }
    }
}
