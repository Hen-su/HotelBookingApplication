
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApplication.Models
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDb;
        public RoomRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext) 
        {
            _hotelBookingApplicationDb = hotelBookingApplicationDbContext;
        }
        public IEnumerable<Room> AllRooms
        {
            get
            {
                return _hotelBookingApplicationDb.Rooms;
            }
        }

        public Room GetRoomById(int roomId)
        {
            return _hotelBookingApplicationDb.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        }
    }
}
