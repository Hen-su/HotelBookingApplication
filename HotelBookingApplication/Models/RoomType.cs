namespace HotelBookingApplication.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeDescription { get; set; }
        public int RoomCapacity { get; set; }
        public decimal RoomRate { get; set; }
    }
}
