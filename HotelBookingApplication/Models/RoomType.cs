namespace HotelBookingApplication.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeDescription { get; set; }
        public int Occupancy { get; set; }
        public decimal RoomRate { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
