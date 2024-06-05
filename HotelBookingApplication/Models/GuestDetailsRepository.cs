
namespace HotelBookingApplication.Models
{
    public class GuestDetailsRepository : IGuestDetailsRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;

        public GuestDetailsRepository (HotelBookingApplicationDbContext hotelBookingApplicationDbContext)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        public IEnumerable<GuestDetails> AllGuests
        {
            get
            {
                return _hotelBookingApplicationDbContext.GuestDetails;
            }
        }

        public void AddGuest(GuestDetails guestDetails)
        {
            _hotelBookingApplicationDbContext.GuestDetails.Add(guestDetails);
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        public GuestDetails GetById(int guestId)
        {
            return _hotelBookingApplicationDbContext.GuestDetails.FirstOrDefault(g => g.GuestDetailsId == guestId);
        }
    }
}
