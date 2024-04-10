using HotelBookingApplication.Models;
using HotelBookingApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomTypeController(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }
        public IActionResult List()
        {
            RoomTypeListViewModel roomTypeListViewModel = new RoomTypeListViewModel(_roomTypeRepository.AllRoomTypes);
            return View(roomTypeListViewModel);
        }

        public IActionResult Details(int id)
        {
            var roomType = _roomTypeRepository.GetRoomTypeById(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }
    }
}
