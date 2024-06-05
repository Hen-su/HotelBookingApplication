using System.ComponentModel;

namespace HotelBookingApplication.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; } = default;
    }
}
