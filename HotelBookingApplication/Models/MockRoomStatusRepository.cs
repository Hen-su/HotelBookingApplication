
namespace HotelBookingApplication.Models
{
    public class MockRoomStatusRepository : IRoomStatusRepository
    {
        public IEnumerable<RoomStatus> AllRoomStatus =>
            new List<RoomStatus>
            {
                new RoomStatus{RoomStatusId = 1, RoomStatusName="Available"},
                new RoomStatus{RoomStatusId = 2, RoomStatusName="Occupied"},
                new RoomStatus{RoomStatusId = 3, RoomStatusName="Reserved"},
                new RoomStatus{RoomStatusId = 4, RoomStatusName="Maintenance"}
            };
    }
}
