using Newtonsoft.Json.Linq;
using BookingClient.BL;
using System.Text.Json.Serialization;

namespace BookingClient.Models
{
    public class Booking
    {
        public bool IsDuplicatStudentPayment { get; set; }

        private bool isConvertToUSD;
        

        public string Reference { get; set; }
        private decimal amount { get; set; }
        private decimal amountReceived { get; set; }

        public string CountryFrom { get; set; }
        public string SenderFullName { get; set; }
        public string SenderAddress { get; set; }
        public string School { get; set; }
        public string CurrencyFrom { get; set; }
        public long StudentId { get; set; }
        public string Email { get; set; }
        public decimal FinalAmount { get { return CommonUtils.CurrencyConversion(amount, CurrencyFrom); }
}
        public decimal FinalAmountReceived { get { return CommonUtils.CurrencyConversion(amountReceived, CurrencyFrom); } }


        public Booking() {
            //Amount = CommonUtils.CurrencyConversion(Amount, CurrencyFrom);
            //AmountReceived = CommonUtils.CurrencyConversion(AmountReceived, CurrencyFrom);
        }

        //[JsonConstructor]
        //public Booking(string reference, string countryFrom,string senderFullName,string senderAddress,string school, string currencyFrom, long studentId, string email, decimal amount, decimal amountReceived)
        //{
        //    //JObject jObject = JObject.Parse(json);
        //    //JToken jBooking = jObject["booking"];
        //    //Reference = (string)jBooking["reference"];
        //    //CountryFrom = (string)jBooking["countryFrom"];
        //    //SenderFullName = (string)jBooking["senderFullName"];
        //    //SenderAddress = (string)jBooking["senderAddress"];
        //    //School = (string)jBooking["school"];
        //    //CurrencyFrom = (string)jBooking["currencyFrom"];
        //    //StudentId = (long)jBooking["studentId"];
        //    //Email = (string)jBooking["email"];
        //    //Amount = CommonUtils.CurrencyConversion( (decimal)jBooking["amount"], CurrencyFrom) ;
        //    //AmountReceived = CommonUtils.CurrencyConversion((decimal)jBooking["amountReceived"], CurrencyFrom);


        //    Reference = reference;
        //    CountryFrom = countryFrom;
        //    SenderFullName = senderFullName;
        //    SenderAddress = senderAddress;
        //    School = school;
        //    CurrencyFrom = currencyFrom;
        //    StudentId = studentId;
        //    Email = email;
        //    Amount = CommonUtils.CurrencyConversion(amount, CurrencyFrom);
        //    AmountReceived = CommonUtils.CurrencyConversion(amountReceived, CurrencyFrom);

        //}

    }
}
