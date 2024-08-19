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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var ticketHandler = new TicketHandler();
                var ticketEntity = ticketHandler.GetTicketById(id.Value);
                var ticketHelper = new TicketHelper();
                var ticketModel = ticketHelper.GetDetailModel(ticketEntity.Data);

                if (ticketModel == null)
                {
                    return NotFound();
                }

                return View(ticketModel);
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
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var name = user.FirstName + " " + user.LastName;
                var helper = new TicketHelper();
                var data = helper.ToDataEntity(ticketModel, name);

                var handler = new TicketHandler();
                var createdTicket = handler.InsertTicket(data).Data;

                return RedirectToAction("Details", new { id = createdTicket.TicketID });
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
            try
            {
                var ticketHandler = new TicketHandler();
                var ticketEntity = ticketHandler.GetTicketById(id.Value);
                var ticketHelper = new TicketHelper();
                var ticketModel = ticketHelper.GetEditModel(ticketEntity.Data);
                return View(ticketModel);
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

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var name = user.FirstName + " " + user.LastName;
                var helper = new TicketHelper();
                var data = helper.ToDataEntity(ticketModel, name);

                var handler = new TicketHandler();
                var postedTicket = handler.UpdateTicket(data).Data;

                return RedirectToAction(nameof(Details), new { id = ticketModel.TicketID });
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

            var ticketModel = await _context.TicketModel
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticketModel == null)
            {
                return NotFound();
            }

            return View(ticketModel);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketModel = await _context.TicketModel.FindAsync(id);
            if (ticketModel != null)
            {
                _context.TicketModel.Remove(ticketModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketModelExists(int id)
        {
            return _context.TicketModel.Any(e => e.TicketID == id);
        }

    }
}
