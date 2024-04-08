using System.ComponentModel;

namespace HotelBookingApplication.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public RoomType RoomType { get; set; } = default;
    }
}
