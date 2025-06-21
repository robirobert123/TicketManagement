using DataAcces;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TicketManagement.Areas.Identity.Data;

namespace TicketManagement.Controllers
{
    [Authorize(Roles = "User")]
    public class UserDashboardController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly UserManager<TicketManagementUser> _userManager;
        private readonly IStatusRepository _statusRepository;
        private readonly IPriorityRepository _priorityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UserDashboardController(
            ITicketRepository ticketRepository,
            UserManager<TicketManagementUser> userManager,
            IStatusRepository statusRepository,
            IPriorityRepository priorityRepository,
            ICategoryRepository categoryRepository)
        {
            _ticketRepository = ticketRepository;
            _userManager = userManager;
            _statusRepository = statusRepository;
            _priorityRepository = priorityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            // Get tickets assigned to or created by the current user
            var userTickets = _ticketRepository.GetAllTickets()
                .Where(t => t.Assignee == currentUser.Id || t.created_User == currentUser.Email)
                .OrderByDescending(t => t.created_Date)
                .ToList();

            // Get statistics
            var myTickets = userTickets.Count();
            var openTickets = userTickets.Count(t => t.Status?.Name?.ToLower() != "done" && t.Status?.Name?.ToLower() != "closed");
            var completedTickets = userTickets.Count(t => t.Status?.Name?.ToLower() == "done" || t.Status?.Name?.ToLower() == "closed");
            var highPriorityTickets = userTickets.Count(t => t.Priority?.Name?.ToLower() == "high" || t.Priority?.Name?.ToLower() == "urgent");

            ViewBag.MyTickets = myTickets;
            ViewBag.OpenTickets = openTickets;
            ViewBag.CompletedTickets = completedTickets;
            ViewBag.HighPriorityTickets = highPriorityTickets;
            ViewBag.UserName = $"{currentUser.FirstName} {currentUser.LastName}";
            ViewBag.UserEmail = currentUser.Email;

            return View(userTickets);
        }

        [HttpGet]
        public IActionResult CreateTicket()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            ViewBag.Priorities = _priorityRepository.GetAllPriorities();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(ticket.Title))
                {
                    ModelState.AddModelError("Title", "Title is required.");
                }
                if (string.IsNullOrWhiteSpace(ticket.Description))
                {
                    ModelState.AddModelError("Description", "Description is required.");
                }
                if (ticket.CategoryID <= 0)
                {
                    ModelState.AddModelError("CategoryID", "Category is required.");
                }
                if (ticket.PriorityID <= 0)
                {
                    ModelState.AddModelError("PriorityID", "Priority is required.");
                }

                if (ModelState.IsValid)
                {
                    // Set default values
                    ticket.created_Date = DateTime.Now;
                    ticket.audit_Date = DateTime.Now;
                    ticket.created_User = currentUser.Email ?? "Unknown";
                    ticket.audit_User = currentUser.Email ?? "Unknown";
                    ticket.Deleted = false;

                    // Set default status (assuming "To Do" or similar exists)
                    var defaultStatus = _statusRepository.GetAllStatus().FirstOrDefault(s => s.Name.ToLower() == "to do" || s.Name.ToLower() == "open" || s.Name.ToLower() == "new");
                    if (defaultStatus != null)
                    {
                        ticket.StatusID = defaultStatus.StatusID;
                    }
                    else
                    {
                        // If no default status found, get the first available status
                        var firstStatus = _statusRepository.GetAllStatus().FirstOrDefault();
                        if (firstStatus != null)
                        {
                            ticket.StatusID = firstStatus.StatusID;
                        }
                        else
                        {
                            ModelState.AddModelError("", "No status available in the system. Please contact administrator.");
                            ViewBag.Categories = _categoryRepository.GetAllCategories();
                            ViewBag.Priorities = _priorityRepository.GetAllPriorities();
                            return View(ticket);
                        }
                    }

                    // Use UnitOfWork pattern to save changes
                    using (var unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.TicketRepository.InsertTicket(ticket);
                        unitOfWork.Save();
                    }

                    TempData["Success"] = "Ticket created successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to create ticket: {ex.Message}";
                // Log the full exception for debugging
                Console.WriteLine($"Error creating ticket: {ex}");
            }

            // If we got this far, something failed, redisplay form
            // Add model state errors to TempData for debugging
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["Error"] = "Validation errors: " + string.Join(", ", errors);
            }

            ViewBag.Categories = _categoryRepository.GetAllCategories();
            ViewBag.Priorities = _priorityRepository.GetAllPriorities();
            return View(ticket);
        }

        [HttpGet]
        public IActionResult ViewTicket(int id)
        {
            var ticket = _ticketRepository.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Check if user has access to this ticket (assigned to them or created by them)
            var currentUserId = _userManager.GetUserId(User);
            var currentUserEmail = User.Identity.Name;

            if (ticket.Assignee != currentUserId && ticket.created_User != currentUserEmail)
            {
                TempData["Error"] = "You don't have permission to view this ticket.";
                return RedirectToAction("Index");
            }

            return View(ticket);
        }
    }
}
