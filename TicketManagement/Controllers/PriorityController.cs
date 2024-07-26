using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.Controllers
{
    public class PriorityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
