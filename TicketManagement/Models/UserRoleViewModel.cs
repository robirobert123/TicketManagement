using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Models
{
    public class UserRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Email { get; set; }

        public List<string> SelectedRoles { get; set; } = new List<string>();

        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}
