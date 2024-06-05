namespace HotelBookingApplication.Models
{
    public class ReservationDetails
    {
        public int ReservationDetailsId { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal Price { get; set; }
    }
}
