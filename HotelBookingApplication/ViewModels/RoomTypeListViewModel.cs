using HotelBookingApplication.Models;

namespace HotelBookingApplication.ViewModels
{
    public class RoomTypeListViewModel
    {
        public IEnumerable<RoomType>? RoomTypes { get; }
        public RoomTypeListViewModel(IEnumerable<RoomType> roomTypes) 
        {
            RoomTypes = roomTypes;
        }
    }
}
