using BL;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingClient.Controllers
{
    [Route("api/payments_with_quality_check")]
    public class PaymentsController : Controller
    {

        [HttpGet(Name = "payments_with_quality_check")]
        public  string Get()
        {
            List<BookingQualityCheck> list= BookingsBL.Instance.GetQualityCheck();

            var customDataObj = new { bookings_with_quality_check = list };
            string json = JsonConvert.SerializeObject(customDataObj);
            return json;
        }
    }
}
