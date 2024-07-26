using BusinessLogic.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Data;
using TicketManagement.Helpers;
using TicketManagement.Models;

namespace TicketManagement.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketManagementDbContext _context;

        public TicketController(TicketManagementDbContext context)
        {
            _context = context;
        }

        // GET: TicketModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketModel.ToListAsync());
        }

        // GET: TicketModels/Details/5
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

        // GET: TicketModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Title,Description,PriorityID,PriorityText,CategoryID,CategoryText,StatusID,StatusText,created_Date,audit_Date,created_User,audit_User,Assignee,AssigneeName,Deleted")] TicketModel ticketModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketModel);
        }

        // GET: TicketModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketModel = await _context.TicketModel.FindAsync(id);
            if (ticketModel == null)
            {
                return NotFound();
            }
            return View(ticketModel);
        }

        // POST: TicketModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,Title,Description,PriorityID,PriorityText,CategoryID,CategoryText,StatusID,StatusText,created_Date,audit_Date,created_User,audit_User,Assignee,AssigneeName,Deleted")] TicketModel ticketModel)
        {
            if (id != ticketModel.TicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketModelExists(ticketModel.TicketID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticketModel);
        }

        // GET: TicketModels/Delete/5
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

        // POST: TicketModels/Delete/5
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
