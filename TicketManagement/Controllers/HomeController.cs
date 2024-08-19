using BusinessLogic.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Helpers;
using TicketManagement.Models;

namespace TicketManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<TicketManagementUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<TicketManagementUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            DashboardModel ticketDashboard = new DashboardModel();
            var ticketHandler = new TicketHandler();
            TicketHelper ticketHelper = new TicketHelper();
            ViewBag.PrioritySelectList = new SelectList(ticketHandler.GetTicketPriorities().Data, "PriorityID", "PriorityName");

            try
            {
                var tickets = ticketHandler.GetAllTickets();
                if (!tickets.IsSuccess)
                {
                    ModelState.AddModelError("Error", tickets.Message);
                    _logger.LogError(tickets.Message);
                }
                else
                {
                    ticketDashboard.TicketDetails = new List<TicketDetailsModel>();
                    foreach (var ticket in tickets.Data)
                    {
                        ticketDashboard.TicketDetails.Add(ticketHelper.GetDetailModel(ticket));
                    }
                }

                var ticketStatuses = ticketHandler.GetTicketStatuses();
                if (!ticketStatuses.IsSuccess)
                {
                    ModelState.AddModelError("Error", ticketStatuses.Message);
                    _logger.LogError(ticketStatuses.Message);
                }
                else
                {
                    ticketDashboard.Statuses = ticketStatuses.Data.ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting ticket details");
            }
            return View(ticketDashboard);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
