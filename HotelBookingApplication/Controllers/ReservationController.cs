using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly IReservationStatusRepository _reservationStatusRepository;

        public ReservationController(IReservationDetailsRepository reservationDetailsRepository, IRoomTypeRepository roomTypeRepository, 
            IRoomRepository roomRepository, IShoppingCart shoppingcart, IGuestDetailsRepository guestDetailsRepository, IReservationRepository reservationRepository, IReservationStatusRepository reservationStatusRepository)
        {
            _reservationDetailsRepository = reservationDetailsRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
            _shoppingcart = shoppingcart;
            _guestDetailsRepository = guestDetailsRepository;
            _reservationRepository = reservationRepository;
            _reservationStatusRepository = reservationStatusRepository;
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
                        if(checkOutDate > details.CheckInDate && checkOutDate <= details.CheckOutDate)
                        {
                            removeRooms.Add(roomList.FirstOrDefault(rm => rm.RoomId == details.RoomId));
                        }
                        
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
                    Email = HttpContext.User.Identity.Name,
                    GuestDetailsId = guest.GuestDetailsId
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

        [Authorize(Roles = "Admin")]
        public IActionResult AllReservations()
        {
            var reservations = _reservationRepository.AllReservations.ToList();
            return View(reservations);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ReservationDetailsPartial(int id)
        {
            var reservationDetails = _reservationDetailsRepository.AllDetails.Where(rd => rd.ReservationId == id);
            return PartialView("_ReservationDetails", reservationDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GuestDetailsPartial(int id)
        {
            var guestDetails = _guestDetailsRepository.AllGuests.FirstOrDefault(g => g.GuestDetailsId == id);
            return PartialView("_GuestDetails", guestDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id) 
        {
            if (id != null)
            {
                var reservation = _reservationRepository.GetById(id);
                return View(reservation);
            }
            return NotFound("Reservation not found");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                var reservation = _reservationRepository.GetById(id);
                ViewBag.ResStatus = new SelectList(_reservationStatusRepository.AllReservationStatus, "ReservationStatusId", "ReservationStatusName", reservation.RSidNavigation);
                return View(reservation);
            }
            return NotFound("Reservation not found");
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryTokenAttribute]
        [HttpPost]
        public IActionResult Edit(int id, [Bind("ReservationId,CreationTime,Email,NumberOfGuests,GuestDetailsId,IsPaid,ReservationStatusId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _reservationRepository.UpdateReservation(reservation);
                return RedirectToAction("AllReservations");
            }
            ViewBag.ResStatus = new SelectList(_reservationStatusRepository.AllReservationStatus, "ReservationStatusId", "ReservationStatusName", reservation.RSidNavigation);
            return View(reservation);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                var reservation = _reservationRepository.GetById(id);
                return View(reservation);
            }
            return NotFound("Reservation not found");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id != null)
            {
                var reservation = _reservationRepository.GetById(id);
                var details = _reservationDetailsRepository.AllDetails.Where(d => d.ReservationId == id);
                if (details.Any())
                {
                    foreach(var item in details)
                    {
                        _reservationDetailsRepository.Delete(item);
                    }
                }
                _reservationRepository.DeleteReservation(reservation);
                return RedirectToAction("AllReservations");
            }
            return RedirectToAction("Delete", id);
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Search()
        {
            return View();
        }
    }

    
}
