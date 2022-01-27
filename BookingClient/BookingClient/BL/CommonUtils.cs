using System.Net;

namespace BookingClient.BL
{
    public static class CommonUtils
    {
        
        private const string USDCurrency = "USD";
        private const string urlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";
        public static decimal CurrencyConversion(decimal amount, string fromCurrency)
        {
                if (fromCurrency == USDCurrency)
                    return amount;

                string url = string.Format(urlPattern, fromCurrency, USDCurrency);

                using (var wc = new WebClient())
                {
                    var json = wc.DownloadString(url);

                    Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(json);
                    decimal exchangeRate = (decimal)token.SelectToken("rate");

                    return (amount * exchangeRate);
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
