namespace HotelBookingApplication.Models
{
    public interface IReservationDetailsRepository
    {
        public IEnumerable<ReservationDetails> AllDetails { get; }
        public void Add(ReservationDetails reservationDetails);
        public void Delete(ReservationDetails reservationDetails);
    }
}
