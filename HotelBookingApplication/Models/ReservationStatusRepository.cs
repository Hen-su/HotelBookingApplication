
namespace HotelBookingApplication.Models
{
    public class ReservationStatusRepository : IReservationStatusRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        public ReservationStatusRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        public IEnumerable<ReservationStatus> AllReservationStatus
        {
            get
            {
                return _hotelBookingApplicationDbContext.ReservationStatuses;
            }
        }
    }
}
