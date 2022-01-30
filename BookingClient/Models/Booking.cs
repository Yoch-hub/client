using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Common;

namespace Models
{
    public class Booking
    {
        private decimal convertedAmount;
        private decimal convertedAmountReceived;
        [JsonProperty("Reference")]
        public string Reference { get; set; }

        [JsonProperty("amount")]
        public decimal? amount { get; set; }

        [JsonProperty("amount_received")]
        public decimal? amountReceived { get; set; }

        [JsonProperty("country_from")] 
        public string CountryFrom { get; set; }

        [JsonProperty("sender_full_name")]
        public string SenderFullName { get; set; }

        [JsonProperty("sender_address")]
        public string SenderAddress { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("currency_from")]
        public string CurrencyFrom { get; set; }

        [JsonProperty("student_id")]
        public long? StudentId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        public bool IsDuplicatStudentPayment { get; set; }

        public decimal ConvertedAmount
        { 
            get 
            { 
                if (convertedAmount == 0) 
                     convertedAmount = CommonUtils.CurrencyConversiontoUSD(amount, CurrencyFrom);

                return convertedAmount;
            } 
        }
        public decimal ConvertedAmountReceived {
            get
            {
                if (convertedAmountReceived == 0)
                    convertedAmountReceived = CommonUtils.CurrencyConversiontoUSD(amountReceived, CurrencyFrom);

                return convertedAmountReceived;
            }
        }
   
    public bool IsValid()
        {
            if (StudentId == null)
                return false ;

            return true;
        }

    }
}
