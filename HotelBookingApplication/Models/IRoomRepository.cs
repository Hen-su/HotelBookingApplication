namespace HotelBookingApplication.Models
{
    public interface IRoomRepository
    {
        IEnumerable<Room> AllRooms { get; }
        Room? GetRoomById(int roomId);
    }
}
