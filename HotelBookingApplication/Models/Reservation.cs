namespace HotelBookingApplication.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime CreationTime { get; set; }
        public string ClientName { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public int RoomId { get; set; }
        public List<int>? RoomIds { get; set; }
        public Room Room { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set;}
        public bool IsPaid { get; set; }
    }
}
