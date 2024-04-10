
namespace HotelBookingApplication.Models
{
    public class MockRoomRepository : IRoomRepository
    {
        private readonly IRoomTypeRepository _roomTypeRepository = new MockRoomTypeRepository();
        private readonly IRoomStatusRepository _roomStatusRepository = new MockRoomStatusRepository();
        public IEnumerable<Room> AllRooms =>
            new List<Room>
            {
                new Room{RoomId = 101, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[0], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 102, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[0], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 103, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[1], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 104, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[2], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 105, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[3], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 201, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[0], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 202, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[0], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 203, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[1], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 204, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[2], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
                new Room{RoomId = 205, RoomType = _roomTypeRepository.AllRoomTypes.ToList()[3], RoomStatus = _roomStatusRepository.AllRoomStatus.ToList()[0]},
            };

        public Room? GetRoomById(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
