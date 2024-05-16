using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
