namespace HotelBookingApplication.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime CreationTime { get; set; }
        public GuestDetails GuestDetails { get; set; } = default;
        public int NumberOfGuests { get; set; }
        public List<ReservationDetails>? ReservationDetails { get; set; }
        public bool IsPaid { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; } = default;
    }
}
