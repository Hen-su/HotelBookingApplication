
namespace HotelBookingApplication.Models
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        public ReservationRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        public IEnumerable<Reservation> AllReservations
        {
            get
            {
                return _hotelBookingApplicationDbContext.Reservations;
            }
        }

        public Reservation GetById(int resId)
        {
            throw new NotImplementedException();
        }

        public Reservation GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
