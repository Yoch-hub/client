using System.Text.Json.Serialization;

namespace BookingClient.Models
{
    public class RootBookings
    {
        public List<Booking> bookings { get; set; }
    }
}
