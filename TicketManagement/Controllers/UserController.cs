using DataAcces;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace TicketManagement.Controllers

{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        public UserController()
        {
            _userRepository = new UserRepository(new TicketManagementEntities());
        }
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public ActionResult Index()
        {
            var model = _userRepository.GetAllUsers();
            return View(model);
        }

        public ActionResult AddUser(string id)
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Add User Failed";
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(AspNetUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.InsertUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
                TempData["Failed"] = "Add User Failed";
            }
            return View(user);
        }
    }
}