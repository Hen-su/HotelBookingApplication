using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace HotelBookingApplication.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        public SearchController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_reservationRepository.AllReservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_reservationRepository.AllReservations.Any(r => r.ReservationId == id))
            {
                return NotFound();
            }
            return Ok(_reservationRepository.AllReservations.Where(r => r.ReservationId == id));
        }

        [HttpPost]
        public IActionResult SearchReservations([FromBody] string searchQuery)
        {
            IEnumerable<Reservation> res = new List<Reservation>();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                res = _reservationRepository.SearchReservations(searchQuery);
            }
            return new JsonResult(res);
        }
    }
}

