using System.Text.Json.Serialization;

namespace Models 
{ 
    public class RootBookings
    {
        public List<Booking> bookings { get; set; }
    }
}
