namespace HotelBookingApplication.Models
{
    public interface IRoomTypeRepository
    {
        IEnumerable<RoomType> AllRoomTypes { get; }
        RoomType GetRoomTypeById(int id);
    }
}
