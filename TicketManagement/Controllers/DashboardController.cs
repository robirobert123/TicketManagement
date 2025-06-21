using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // Redirect users based on their role
            if (User.IsInRole("Admin") || User.IsInRole("Developer"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "UserDashboard");
            }
            else
            {
                // If user has no role or unknown role, redirect to user dashboard
                return RedirectToAction("Index", "UserDashboard");
            }
        }
    }
}
