using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
