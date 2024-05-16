using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository, IRoomTypeRepository roomTypeRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Availability(DateOnly? checkInDate, DateOnly? checkOutDate)
        {
            List<RoomType> returnList = new List<RoomType>();
            if (checkInDate != null && checkOutDate != null)
            {
                string[] checkIn = checkInDate.ToString().Split("/");
                string[] checkOut = checkOutDate.ToString().Split("/");
                string setCheckIn = string.Join("-", checkIn.Reverse());
                string setCheckOut = string.Join("-", checkOut.Reverse());
                ViewBag.CheckIn = setCheckIn;
                ViewBag.CheckOut = setCheckOut;
                IEnumerable<Reservation> reservations = _reservationRepository.AllReservations.OrderBy(res => res.CheckInDate);
                IEnumerable<Room> rooms = _roomRepository.AllRooms.OrderBy(rm => rm.RoomId);
                IEnumerable<RoomType> roomTypes = _roomTypeRepository.AllRoomTypes;
                List<Room> roomList = rooms.ToList();
                List<Room> removeRooms = new List<Room>();
                Dictionary<int, int> roomCount = new Dictionary<int, int>();
                
                foreach (RoomType roomType in roomTypes)
                {
                    roomCount.Add(roomType.RoomTypeId, 0);
                }
                
                foreach (Reservation reservation in reservations)
                {
                    if (checkInDate >= reservation.CheckInDate && checkInDate < reservation.CheckOutDate)
                    {
                        removeRooms.Add(roomList.FirstOrDefault(rm => rm.RoomId == reservation.RoomId));
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
            
            //get all reservations that are unpaid
            //get a list of all rooms
            //remove rooms with reservations in this date range
            //return all roomtypes that are available
            return View(returnList);
        }
    }
}
