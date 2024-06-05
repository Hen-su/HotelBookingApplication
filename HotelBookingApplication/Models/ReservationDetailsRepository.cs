

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
    }
}
