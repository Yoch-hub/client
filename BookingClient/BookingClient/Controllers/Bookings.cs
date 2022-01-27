using BookingClient.BL;
using BookingClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClient.Controllers
{
    [Route("api/payments_with_quality_check")]
    public class AlbumController : Controller
    {

        [HttpGet(Name = "payments_with_quality_check")]
        public List<BookingQualityCheck> Get()
        {
            return BookingsBL.Instance.GetQualityCheck();
        }
    }
}
