using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Models
{
    public class TicketEditModel
    {
        [Key]
        public int TicketID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [BindProperty]
        [DisplayName("Priority")]
        public int PriorityID { get; set; }
        [BindProperty]
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DisplayName("Created at:")]
        public System.DateTime CreatedDate { get; set; }
        [DisplayName("Created by:")]
        public string CreatedUser { get; set; }
        [BindProperty]
        [DisplayName("Status")]
        public int StatusID { get; set; }
        [BindProperty]
        [DisplayName("Assignee")]
        public string Assignee { get; set; }
        [NotMapped]
        public List<SelectListItem> TicketStatuses { get; set; }
        [NotMapped]
        public List<SelectListItem> TicketCategories { get; set; }
        [NotMapped]
        public List<SelectListItem> TicketPriorities { get; set; }
        [NotMapped]
        public List<SelectListItem> TicketAssignees { get; set; }
        [NotMapped]
        public List<string> ErrorMessages { get; set; }
        public bool IsValid { get; set; }
    }
}
