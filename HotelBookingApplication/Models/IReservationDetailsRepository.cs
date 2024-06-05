namespace HotelBookingApplication.Models
{
    public interface IReservationDetailsRepository
    {
        public IEnumerable<ReservationDetails> AllDetails { get; }
    }
}
