using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
