
namespace HotelBookingApplication.Models
{
    public class RoomStatusRepository : IRoomStatusRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        public RoomStatusRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        public IEnumerable<RoomStatus> AllRoomStatus
        {
            get
            {
                return _hotelBookingApplicationDbContext.RoomStatuses;
            }
        }
    }
}
