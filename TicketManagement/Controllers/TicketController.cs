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
                }
                else
                {
                    var ticketModel = ticketHelper.GetDetailModel(ticketEntity.Data);
                    if (ticketModel == null)
                    {
                        return NotFound();
                    }

                    return View(ticketModel);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting details for ticket ID");
                ModelState.AddModelError("Error", e.Message);
            }
            return RedirectToAction(nameof(Index));
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketHandler = new TicketHandler();
            try
            {
                var result = ticketHandler.DeleteTicket(id);
                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    ModelState.AddModelError("Error", result.Message);
                    return RedirectToAction(nameof(Details), new { id });
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting ticket ID {0}", id);
                ModelState.AddModelError("Error", e.Message);
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        private bool TicketModelExists(int id)
        {
            return _context.TicketModel.Any(e => e.TicketID == id);
        }

        public bool AddComment(int ticketId, string commentText)
        {
            var handler = new TicketHandler();
            try
            {
                var ticket = handler.GetTicketById(ticketId);
                if (!ticket.IsSuccess)
                {
                    _logger.LogError(ticket.Message);
                }
                else
                {
                    var user = _userManager.GetUserAsync(User).Result;
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
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding comment to ticket ID");
                return false;
            }
        }
    }
}
