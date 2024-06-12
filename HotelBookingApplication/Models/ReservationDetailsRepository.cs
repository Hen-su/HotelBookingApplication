

namespace HotelBookingApplication.Models
{
    public class ReservationDetailsRepository : IReservationDetailsRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        public ReservationDetailsRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        IEnumerable<ReservationDetails> IReservationDetailsRepository.AllDetails
        {
            get
            {
                return _hotelBookingApplicationDbContext.ReservationsDetails;
            }
        }

        public void Add(ReservationDetails reservationDetails)
        {
            _hotelBookingApplicationDbContext.Add(reservationDetails);
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        public void Delete(ReservationDetails reservationDetails)
        {
            _hotelBookingApplicationDbContext.ReservationsDetails.Remove(reservationDetails);
            _hotelBookingApplicationDbContext.SaveChanges() ;
        }
    }
}
