using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BookingTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void GetQualityCheck_ShouldReturnCorrectDuplicate()
        {
            List<Booking> bookings = GetTestBookings();
            BookingsBL.Instance.SetDuplicateValue(ref bookings);
          
            Assert.AreEqual(bookings[0].IsDuplicatStudentPayment,true);
        }

        [TestMethod]
        public void GetQualityCheck_ShouldReturnCorrectConvert()
        {
            List<Booking> bookings = GetTestBookings();

            Assert.AreEqual(Math.Round(bookings[1].ConvertedAmount,0), (decimal)Math.Round(44593.00, 0));
            Assert.AreEqual(Math.Round(bookings[1].ConvertedAmountReceived, 0), (decimal)Math.Round(1114827201.78, 0));
        }

        [TestMethod]
        public void GetQualityCheck_ShouldReturnCorrectAmountWithFees()
        {
            List<Booking> bookings = GetTestBookings();
            BookingQualityCheck bookingCheck = new BookingQualityCheck(bookings[2]);

            Assert.AreEqual(Math.Round(bookingCheck.amountWithFees, 0),(decimal) Math.Round(1210.0749, 0));
        }

        [TestMethod]
        public void GetQualityCheck_ShouldReturnCorrectQualityCheck()
        {
            List<Booking> bookings = GetTestBookings();
            BookingsBL.Instance.SetDuplicateValue(ref bookings);
            List<BookingQualityCheck>  bookingCheckList = bookings.Select(booking => new BookingQualityCheck(booking)).ToList(); 

            Assert.AreEqual(bookingCheckList[0].qualityCheck, "InvalidEmail,DuplicatedPayment");
            Assert.AreEqual(bookingCheckList[1].qualityCheck, "AmountThreshold");
        }

        private List<Booking> GetTestBookings()
        {
            var testBookings = new List<Booking>();
            testBookings.Add(new Booking { Reference = "10000", StudentId = 1, amount = 1500, amountReceived = 1600, CountryFrom = "China", CurrencyFrom = "USD", Email = "tttttt.tt", School = "MIT", SenderAddress = "", SenderFullName = "Yochi Liberman" });
            testBookings.Add(new Booking { Reference = "50000", StudentId = 1000, amount = 40000, amountReceived = 1000000000, CountryFrom = "Spain", CurrencyFrom = "Eur", Email = "test@test.test", School = "MIT", SenderAddress = "", SenderFullName = "Ben Blass" });
            testBookings.Add(new Booking { Reference = "", StudentId = 1, amount = 1500, amountReceived = 1600, CountryFrom = "China", CurrencyFrom = "CAD", Email = "tttttt.tt", School = "MIT", SenderAddress = "", SenderFullName = "kuku" });
           
            return testBookings;
        }
    }
}