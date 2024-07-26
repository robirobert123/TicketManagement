using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
