using Newtonsoft.Json.Linq;
using System.Net;

namespace BL
{
    public static class CommonUtils
    {
        private const string DefaultCurrency = "USD";
        private const string urljson = "https://freecurrencyapi.net/api/v2/latest?apikey=0dd09760-8020-11ec-8898-399a7c1c754a";
        public static decimal CurrencyConversiontoUSD(decimal? amount, string fromCurrency)
        {
            try
            {
                //check if exists value
                if (amount == null)
                    return 0;

                //if currency  equal to default - no convert
                if (fromCurrency == DefaultCurrency)
                    return (decimal)amount;

                //if no currency - set USD as default    
                if (fromCurrency == String.Empty)
                    fromCurrency = DefaultCurrency;

                //convert    
                using (var webClient = new WebClient())
                {
                    var jsonString = webClient.DownloadString(urljson);
                    JObject o = JObject.Parse(jsonString);
                    decimal exchangeRate = (decimal)o.SelectToken(@"data." + fromCurrency.ToUpper());

                    return (decimal)(amount / exchangeRate);
                }
            }
            catch(Exception ex)
            {
                return 0;
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
