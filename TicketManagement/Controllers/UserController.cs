using DataAcces;
using DataAcces.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Models;
namespace TicketManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private UserManager<TicketManagementUser> _userManager;
        public UserController(IRoleRepository roleRepository, UserManager<TicketManagementUser> userManager, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userManager = userManager;
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
        public async Task<ActionResult> AddUser(AspNetUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.InsertUser(user);
                    TicketManagementUser ticketManagementUser = new TicketManagementUser
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                    //insert role
                    await _userManager.AddToRoleAsync(ticketManagementUser, "User");
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageRoles()
        {
            var users = _userManager.Users.ToList();
            var userRolesModel = new List<UserRolesModel>();
            foreach (var user in users)
            {
                var thisViewModel = new UserRolesModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };
                userRolesModel.Add(thisViewModel);
            }
            var roles = _roleRepository.GetAllRoles().Select(r => r.Name).ToList();
            ViewBag.Roles = roles;
            return View(userRolesModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("ManageRoles");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleRepository.GetAllRoles().Select(r => r.Name).ToList();

            var model = new UserRoleViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                SelectedRoles = userRoles.ToList(),
                AllRoles = allRoles.Select(role => new SelectListItem
                {
                    Value = role,
                    Text = role,
                    Selected = userRoles.Contains(role)
                }).ToList()
            };

            return View("ManageUserRoles", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoles(UserRoleViewModel model)
        {
            try
            {
                // Validate user exists
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    TempData["Error"] = "User not found.";
                    return RedirectToAction("ManageRoles");
                }

                // Validate model
                if (!ModelState.IsValid)
                {
                    // Repopulate AllRoles for the view
                    var allRoles = _roleRepository.GetAllRoles().Select(r => r.Name).ToList();
                    model.AllRoles = allRoles.Select(role => new SelectListItem
                    {
                        Value = role,
                        Text = role,
                        Selected = model.SelectedRoles?.Contains(role) ?? false
                    }).ToList();

                    return View("ManageUserRoles", model);
                }

                // Get current and selected roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                var selectedRoles = model.SelectedRoles ?? new List<string>();

                // Validate that selected roles exist
                var allAvailableRoles = _roleRepository.GetAllRoles().Select(r => r.Name).ToList();
                var invalidRoles = selectedRoles.Where(r => !allAvailableRoles.Contains(r)).ToList();

                if (invalidRoles.Any())
                {
                    TempData["Error"] = $"Invalid roles selected: {string.Join(", ", invalidRoles)}";
                    return RedirectToAction("ManageUserRoles", new { userId = model.UserId });
                }

                // Remove current roles that are not in the selected list
                var rolesToRemove = currentRoles.Where(r => !selectedRoles.Contains(r)).ToList();
                if (rolesToRemove.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    if (!removeResult.Succeeded)
                    {
                        TempData["Error"] = $"Failed to remove roles: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}";
                        return RedirectToAction("ManageUserRoles", new { userId = model.UserId });
                    }
                }

                // Add new roles that are not currently assigned
                var rolesToAdd = selectedRoles.Where(r => !currentRoles.Contains(r)).ToList();
                if (rolesToAdd.Any())
                {
                    var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    if (!addResult.Succeeded)
                    {
                        TempData["Error"] = $"Failed to add roles: {string.Join(", ", addResult.Errors.Select(e => e.Description))}";
                        return RedirectToAction("ManageUserRoles", new { userId = model.UserId });
                    }
                }

                TempData["Success"] = $"Roles updated successfully for {user.Email}!";
                return RedirectToAction("ManageRoles");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while updating roles: {ex.Message}";
                return RedirectToAction("ManageUserRoles", new { userId = model.UserId });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole(string UserId, string SelectedRole)
        {
            try
            {
                // Validate user exists
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    TempData["Error"] = "User not found.";
                    return RedirectToAction("ManageRoles");
                }

                // Validate role exists (if a role is selected)
                if (!string.IsNullOrEmpty(SelectedRole))
                {
                    var allAvailableRoles = _roleRepository.GetAllRoles().Select(r => r.Name).ToList();
                    if (!allAvailableRoles.Contains(SelectedRole))
                    {
                        TempData["Error"] = $"Invalid role selected: {SelectedRole}";
                        return RedirectToAction("ManageRoles");
                    }
                }

                // Get current roles
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Remove all current roles
                if (currentRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        TempData["Error"] = $"Failed to remove current roles: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}";
                        return RedirectToAction("ManageRoles");
                    }
                }

                // Add the new role (if one is selected)
                if (!string.IsNullOrEmpty(SelectedRole))
                {
                    var addResult = await _userManager.AddToRoleAsync(user, SelectedRole);
                    if (!addResult.Succeeded)
                    {
                        TempData["Error"] = $"Failed to assign role: {string.Join(", ", addResult.Errors.Select(e => e.Description))}";
                        return RedirectToAction("ManageRoles");
                    }
                }

                var roleMessage = string.IsNullOrEmpty(SelectedRole) ? "removed from all roles" : $"assigned to role '{SelectedRole}'";
                TempData["Success"] = $"User {user.Email} has been {roleMessage} successfully!";
                return RedirectToAction("ManageRoles");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while updating the user role: {ex.Message}";
                return RedirectToAction("ManageRoles");
            }
        }
    }
}