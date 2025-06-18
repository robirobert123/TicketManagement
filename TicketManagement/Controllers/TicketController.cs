using BusinessLogic.Handlers;
using DataAcces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Data;
using TicketManagement.Helpers;
using TicketManagement.Models;

namespace TicketManagement.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<TicketManagementUser> _userManager;
        private readonly TicketManagementDbContext _context;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger, TicketManagementDbContext context, UserManager<TicketManagementUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.TicketModel.ToListAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting ticket list");
                ModelState.AddModelError("Error", e.Message);
            }
            return View(new List<Ticket>());
        }

        // GET: Ticket/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHandler = new TicketHandler();
            try
            {
                var ticketEntity = ticketHandler.GetTicketById(id.Value);
                var ticketHelper = new TicketHelper();

                if (!ticketEntity.IsSuccess)
                {
                    ModelState.AddModelError("Error", ticketEntity.Message);
                    _logger.LogError(ticketEntity.Message);
                    return RedirectToAction(nameof(Index));
                }

                var ticketModel = ticketHelper.GetDetailModel(ticketEntity.Data);

                // Ensure comments have user information
                if (ticketModel.Comments != null)
                {
                    foreach (var comment in ticketModel.Comments)
                    {
                        if (string.IsNullOrEmpty(comment.CommentUser))
                        {
                            // Get user info if missing
                            var userInfo = _userManager.FindByIdAsync(comment.CommentUserID).Result;
                            if (userInfo != null)
                            {
                                comment.CommentUser = userInfo.FirstName + " " + userInfo.LastName;
                            }
                        }
                    }
                }

                if (ticketModel == null)
                {
                    return NotFound();
                }

                return View(ticketModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting ticket details");
                ModelState.AddModelError("Error", e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            var model = new TicketCreateModel();
            try
            {
                var helper = new TicketHelper();
                model = helper.GetCreateModel();

                if (!model.IsValid)
                {
                    foreach (var error in model.ErrorMessages)
                    {
                        ModelState.AddModelError("Error", error);
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
            }
            return View(model);
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,PriorityID,CategoryID,StatusID,Assignee")] TicketCreateModel ticketModel)
        {
            var handler = new TicketHandler();
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var name = user.FirstName + " " + user.LastName;
                var helper = new TicketHelper();
                var data = helper.ToDataEntity(ticketModel, name);
                var createdTicket = handler.InsertTicket(data);

                if (!createdTicket.IsSuccess)
                {
                    _logger.LogError(createdTicket.Message);
                }
                else
                {
                    return RedirectToAction("Details", new { id = createdTicket.Data.TicketID });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while creating a new ticket");
                ModelState.AddModelError("Error", e.Message);
            }
            return View(ticketModel);
        }

        // GET: Ticket/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHandler = new TicketHandler();

            try
            {
                var ticketEntity = ticketHandler.GetTicketById(id.Value);
                var ticketHelper = new TicketHelper();
                var ticketModel = ticketHelper.GetEditModel(ticketEntity.Data);
                if (!ticketEntity.IsSuccess)
                {
                    _logger.LogError(ticketEntity.Message);
                }
                else
                {
                    return View(ticketModel);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while loading edit form for ticket ID");
                ModelState.AddModelError("Error", e.Message);
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,Title,Description,PriorityID,PriorityText,CategoryID,CategoryText,StatusID,StatusText,CreatedDate,AuditDate,CreatedUser,AuditUser,Assignee,AssigneeName,Deleted")] TicketEditModel ticketModel)
        {
            if (id != ticketModel.TicketID)
            {
                return NotFound();
            }

            var handler = new TicketHandler();

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var name = user.FirstName + " " + user.LastName;
                var helper = new TicketHelper();
                var data = helper.ToDataEntity(ticketModel, name);

                var postedTicket = handler.UpdateTicket(data);
                if (!postedTicket.IsSuccess)
                {
                    _logger.LogError(postedTicket.Message);
                }
                else
                {
                    return RedirectToAction(nameof(Details), new { id = ticketModel.TicketID });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating ticket ID");
                ModelState.AddModelError("Error", e.Message);
            }
            return View(ticketModel);
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHandler = new TicketHandler();
            try
            {
                var ticketEntity = ticketHandler.GetTicketById(id.Value);
                var ticketHelper = new TicketHelper();

                if (!ticketEntity.IsSuccess)
                {
                    ModelState.AddModelError("Error", ticketEntity.Message);
                    _logger.LogError(ticketEntity.Message);
                    return RedirectToAction(nameof(Index));
                }

                var ticketModel = ticketHelper.GetDetailModel(ticketEntity.Data);
                if (ticketModel == null)
                {
                    return NotFound();
                }

                return View(ticketModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting ticket for deletion");
                ModelState.AddModelError("Error", e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ticketHandler = new TicketHandler();
            try
            {
                var result = ticketHandler.DeleteTicket(id);
                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id });
                }

                TempData["SuccessMessage"] = "Ticket deleted successfully";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting ticket ID {0}", id);
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        private bool TicketModelExists(int id)
        {
            return _context.TicketModel.Any(e => e.TicketID == id);
        }

        [HttpPost]
        public IActionResult AddComment(int ticketId, string commentText)
        {
            var handler = new TicketHandler();
            try
            {
                var ticket = handler.GetTicketById(ticketId);
                if (!ticket.IsSuccess)
                {
                    _logger.LogError(ticket.Message);
                    return Json(0);
                }

                var user = _userManager.GetUserAsync(User).Result;

                // Ensure we have the user's ID
                if (user == null)
                {
                    _logger.LogError("User not found");
                    return Json(0);
                }

                var comment = new Comment
                {
                    CommentText = commentText,
                    TicketID = ticketId,
                    UserID = user.Id,
                    PostedAt = DateTime.Now
                };

                var insertedComment = handler.InsertTicketComment(comment);
                if (!insertedComment.IsSuccess)
                {
                    _logger.LogError(insertedComment.Message);
                    return Json(0);
                }

                return Json(1);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding comment to ticket ID");
                return Json(0);
            }
        }

        // POST: Ticket/UpdateStatus
        [HttpPost]
        public IActionResult UpdateStatus(int ticketId, int statusId)
        {
            try
            {
                var ticketHandler = new TicketHandler();
                var ticketEntity = ticketHandler.GetTicketById(ticketId);

                if (!ticketEntity.IsSuccess || ticketEntity.Data == null)
                {
                    return Json(new { success = false, message = "Ticket not found" });
                }

                var user = _userManager.GetUserAsync(User).Result;
                var name = user.FirstName + " " + user.LastName;

                var ticket = ticketEntity.Data;
                ticket.StatusEntity.StatusID = statusId;

                var helper = new TicketHelper();
                var data = helper.ToDataEntity(new TicketEditModel
                {
                    TicketID = ticket.TicketID,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    PriorityID = ticket.PriorityEntity.PriorityID,
                    CategoryID = ticket.CategoryEntity.CategoryID,
                    StatusID = statusId,
                    Assignee = ticket.AssigneeEntity?.Id,
                    CreatedDate = ticket.created_Date,
                    CreatedUser = ticket.created_User
                }, name);

                var result = ticketHandler.UpdateTicket(data);

                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    return Json(new { success = false, message = result.Message });
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ticket status");
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Add this new endpoint to get dropdown options
        [HttpGet]
        public IActionResult GetDropdownOptions(int ticketId)
        {
            try
            {
                var ticketHandler = new TicketHandler();
                var ticketHelper = new TicketHelper();

                // Get all dropdown options
                var ticketStatusesResult = ticketHandler.GetTicketStatuses();
                var ticketCategoriesResult = ticketHandler.GetTicketCategories();
                var ticketPrioritiesResult = ticketHandler.GetTicketPriorities();
                var ticketUsersResult = ticketHandler.GetTicketUsers();

                // Format for select lists
                var statuses = ticketStatusesResult.Data.Select(s => new { value = s.StatusID.ToString(), text = s.StatusName }).ToList();
                var categories = ticketCategoriesResult.Data.Select(c => new { value = c.CategoryID.ToString(), text = c.CategoryName }).ToList();
                var priorities = ticketPrioritiesResult.Data.Select(p => new { value = p.PriorityID.ToString(), text = p.PriorityName }).ToList();
                var assignees = ticketUsersResult.Data.Select(u => new { value = u.Id, text = u.FirstName + " " + u.LastName }).ToList();

                // Add "Unassigned" option to assignees
                assignees.Insert(0, new { value = "", text = "Unassigned" });

                return Json(new
                {
                    statuses = statuses,
                    categories = categories,
                    priorities = priorities,
                    assignees = assignees
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dropdown options");
                return Json(new { error = "Failed to load dropdown options" });
            }
        }

        // Add this new endpoint to handle inline updates
        [HttpPost]
        public IActionResult UpdateInline(int TicketID, string Title, string Description,
            int PriorityID, int CategoryID, int StatusID, string Assignee,
            DateTime CreatedDate, string CreatedUser)
        {
            try
            {
                var ticketHandler = new TicketHandler();
                var user = _userManager.GetUserAsync(User).Result;
                var name = user.FirstName + " " + user.LastName;

                // Create ticket data entity
                var ticket = new Ticket
                {
                    TicketID = TicketID,
                    Title = Title,
                    Description = Description,
                    PriorityID = PriorityID,
                    CategoryID = CategoryID,
                    StatusID = StatusID,
                    Assignee = Assignee,
                    created_Date = CreatedDate,
                    created_User = CreatedUser,
                    audit_Date = DateTime.Now,
                    audit_User = name,
                    Deleted = false
                };

                // Update ticket
                var result = ticketHandler.UpdateTicket(ticket);

                if (result.IsSuccess)
                {
                    // Get updated ticket to return updated values
                    var updatedTicket = ticketHandler.GetTicketById(TicketID);

                    return Json(new
                    {
                        success = true,
                        message = "Ticket updated successfully",
                        auditDate = updatedTicket.Data.audit_Date.ToString("yyyy-MM-dd")
                    });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ticket inline");
                return Json(new { success = false, message = "An error occurred while updating the ticket" });
            }
        }
    }
}
