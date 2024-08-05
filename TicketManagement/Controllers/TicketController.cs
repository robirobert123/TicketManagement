using BusinessLogic.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public TicketController(TicketManagementDbContext context, UserManager<TicketManagementUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {

            return View(await _context.TicketModel.ToListAsync());
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketHandler = new TicketHandler();
            var ticketEntity = ticketHandler.GetTicketById(id.Value);
            var ticketHelper = new TicketHelper();
            var ticketModel = ticketHelper.ToViewModel(ticketEntity.Data);

            if (ticketModel == null)
            {
                return NotFound();
            }
            return View(ticketModel);

        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            var helper = new TicketHelper();
            var model = helper.ToCreateModel();
            return View(model);
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,PriorityID,CategoryID,StatusID,Assignee")] TicketCreateModel ticketModel)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var name = user.FirstName + " " + user.LastName;
            var helper = new TicketHelper();
            var data = helper.ToDataEntity(ticketModel, name);

            var handler = new TicketHandler();
            var createdTicket = handler.InsertTicket(data).Data;

            return RedirectToAction("Details", new { id = createdTicket.TicketID });
        }

        // GET: Ticket/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticketHandler = new TicketHandler();
            var ticketEntity = ticketHandler.GetTicketById(id.Value);
            var ticketHelper = new TicketHelper();
            var ticketModel = ticketHelper.ToEditModel(ticketEntity.Data);
            return View(ticketModel);
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

            var user = _userManager.GetUserAsync(User).Result;
            var name = user.FirstName + " " + user.LastName;
            var helper = new TicketHelper();
            var data = helper.ToDataEntity(ticketModel, name);

            var handler = new TicketHandler();
            var postedTicket = handler.UpdateTicket(data).Data;
            return RedirectToAction(nameof(Details), new {id = ticketModel.TicketID});
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
