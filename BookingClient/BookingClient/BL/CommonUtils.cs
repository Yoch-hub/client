using Newtonsoft.Json.Linq;
using System.Net;

namespace BookingClient.BL
{
    public static class CommonUtils
    {
        
        private const string urljson = "https://freecurrencyapi.net/api/v2/latest?apikey=0dd09760-8020-11ec-8898-399a7c1c754a";
        public static decimal CurrencyConversiontoUSD(decimal? amount, string fromCurrency)
        {
                if (amount == null)
                    return 0;

                if (fromCurrency == "USD")
                    return (decimal)amount;

                using (var webClient = new WebClient())
                {
                    var jsonString = webClient.DownloadString(urljson);
                    JObject o = JObject.Parse(jsonString);
                    decimal exchangeRate = (decimal)o.SelectToken(@"data."+ fromCurrency.ToUpper());

                    return (decimal)(amount / exchangeRate);
                }
        }

        public static bool IsValidEmail(string email)
        {
            if (email == null)
                return false;
           
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
                return false; 
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
