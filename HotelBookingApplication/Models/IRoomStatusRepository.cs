namespace HotelBookingApplication.Models
{
    public interface IRoomStatusRepository
    {
        IEnumerable<RoomStatus> AllRoomStatus {  get; }
    }
}
