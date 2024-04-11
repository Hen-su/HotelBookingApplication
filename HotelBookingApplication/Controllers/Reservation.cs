using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class Reservation : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckDates(DateTime checkInDate, DateTime checkOutDate)
        {
            string checkIn = checkInDate.ToString();
            string checkOut = checkOutDate.ToString();
            //get all reservations that are unpaid
            //get a list of all rooms
            //remove rooms with reservations in this date range
            //return all roomtypes that are available
            return View();
        }
    }
}
