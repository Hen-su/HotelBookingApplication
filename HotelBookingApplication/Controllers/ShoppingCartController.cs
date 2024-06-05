using HotelBookingApplication.Models;
using HotelBookingApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IRoomTypeRepository roomTypeRepository, IRoomRepository roomRepository, IReservationRepository reservationRepository, IShoppingCart shoppingCart)
        {
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());
            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public IActionResult AddToShoppingCart( int id, DateOnly checkInDate,  DateOnly checkOutDate)
        {
            try
            {
                var selectedRoom = _roomTypeRepository.AllRoomTypes.FirstOrDefault(r =>  r.RoomTypeId == id);
                if (selectedRoom != null)
                {
                    _shoppingCart.AddToCart(selectedRoom, checkInDate, checkOutDate);
                }
                return Json(new {success = true});
            }
            catch
            {
                return Json(new { success = false, error = "Room could not be added to cart. Please try again" });
            }
        }

        public RedirectToActionResult RemoveFromShoppingCart(int roomTypeId)
        {
            var selectedRoom = _roomTypeRepository.AllRoomTypes.FirstOrDefault(r => r.RoomTypeId == roomTypeId);
            if (selectedRoom != null)
            {
                _shoppingCart.RemoveFromCart(selectedRoom);
            }
            return RedirectToAction("Index");
        }
    }
}
