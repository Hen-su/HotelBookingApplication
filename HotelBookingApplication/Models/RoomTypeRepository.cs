
namespace HotelBookingApplication.Models
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        public RoomTypeRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext) 
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        public IEnumerable<RoomType> AllRoomTypes
        {
            get
            {
                return _hotelBookingApplicationDbContext.RoomsTypes;
            }
        }

        public RoomType GetRoomTypeById(int id)
        {
            return _hotelBookingApplicationDbContext.RoomsTypes.FirstOrDefault(rt => rt.RoomTypeId == id);
        }
    }
}
