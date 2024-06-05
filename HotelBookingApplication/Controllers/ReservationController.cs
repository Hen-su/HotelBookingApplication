using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IReservationDetailsRepository _reservationDetailsRepository;
        private readonly IShoppingCart _shoppingcart;
        private readonly IGuestDetailsRepository _guestDetailsRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationDetailsRepository reservationDetailsRepository, IRoomTypeRepository roomTypeRepository, 
            IRoomRepository roomRepository, IShoppingCart shoppingcart, IGuestDetailsRepository guestDetailsRepository, IReservationRepository reservationRepository)
        {
            _reservationDetailsRepository = reservationDetailsRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
            _shoppingcart = shoppingcart;
            _guestDetailsRepository = guestDetailsRepository;
            _reservationRepository = reservationRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Availability(DateOnly? checkInDate, DateOnly? checkOutDate)
        {
            List<RoomType> returnList = new List<RoomType>();
            Dictionary<int, int> roomCount = new Dictionary<int, int>();
            if (checkInDate != null && checkOutDate != null)
            {
                string[] checkIn = checkInDate.ToString().Split("/");
                string[] checkOut = checkOutDate.ToString().Split("/");
                string checkInDay = checkIn[0];
                string checkOutDay = checkOut[0];
                if (checkInDay.Length < 2)
                {
                    checkIn[0] = "0" + checkInDay;
                }
                if (checkOutDay.Length < 2)
                {
                    checkOut[0] = "0" + checkOutDay;
                }
                string setCheckIn = string.Join("-", checkIn.Reverse());
                string setCheckOut = string.Join("-", checkOut.Reverse());
                ViewBag.CheckIn = setCheckIn;
                ViewBag.CheckOut = setCheckOut;
                List<ReservationDetails> reservationDetails = _reservationDetailsRepository.AllDetails.ToList();
                IEnumerable<RoomType> roomTypes = _roomTypeRepository.AllRoomTypes;
                List<Room> roomList = _roomRepository.AllRooms.ToList();
                List<Room> removeRooms = new List<Room>();
                
                
                foreach (RoomType roomType in roomTypes)
                {
                    roomCount.Add(roomType.RoomTypeId, 0);
                }
                
                foreach (ReservationDetails details in reservationDetails)
                {
                    if (checkInDate >= details.CheckInDate && checkInDate < details.CheckOutDate)
                    {
                        removeRooms.Add(roomList.FirstOrDefault(rm => rm.RoomId == details.RoomId));
                    }
                }
                foreach (Room room in removeRooms)
                {
                    roomList.Remove(room);
                }
                foreach (Room room in roomList)
                {
                    roomCount[room.RoomTypeId]++;
                }
                foreach (RoomType roomType in roomTypes)
                {
                    if (roomCount[roomType.RoomTypeId] > 0)
                    {
                        returnList.Add(roomType);
                    }
                }
            }
            //get a list of all rooms
            //remove rooms with reservations in this date range
            //return all roomtypes that are available
            ViewBag.RoomCount = roomCount;
            return View(returnList);
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(GuestDetails guest)
        {
            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItems = items;
            if (_shoppingcart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "You have not selected a room");
            }
            if (ModelState.IsValid)
            {
                _guestDetailsRepository.AddGuest(guest);
                Reservation reservation = new Reservation
                {
                    GuestDetails = guest
                };
                _reservationRepository.MakeReservation(reservation);
                _shoppingcart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(guest);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Your booking has been confirmed";
            return View();
        }
    }
}
