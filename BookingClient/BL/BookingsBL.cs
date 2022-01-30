﻿using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace BL
{
    public  class BookingsBL
    {
        private static BookingsBL instance = null;
        private readonly IConfiguration Configuration;
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

                using (WebClient webClient = new System.Net.WebClient())
                {
                    var jsonString = webClient.DownloadString(url);
                    var root = JsonConvert.DeserializeObject<RootBookings>(jsonString);

                    return root.bookings;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void SetDuplicateValue(ref List<Booking> bookings)
        {
            //get duplictedBooking
            var query = bookings.GroupBy(b => b.StudentId).Where(s => s.Count() > 1).ToDictionary(x => x.Key, y => y.Count());
            //set true on duplicate key
            var duplicates = bookings.Where(book => query.ContainsKey(book.StudentId)).ToList().Any(b => b.IsDuplicatStudentPayment = true);
        }
    }
}
