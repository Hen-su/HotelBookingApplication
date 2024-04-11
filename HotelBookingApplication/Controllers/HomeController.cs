using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
