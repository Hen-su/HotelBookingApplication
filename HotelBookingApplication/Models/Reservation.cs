namespace HotelBookingApplication.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime CreationTime { get; set; }
        public string ClientName { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public List<int> RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set;}
        public bool IsPaid { get; set; }
    }
}
