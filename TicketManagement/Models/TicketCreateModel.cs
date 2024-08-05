using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Models
{
    public class TicketCreateModel
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
        [BindProperty]
        [DisplayName("Status")]
        public int StatusID { get; set; }
        [BindProperty]
        [DisplayName("Assignee")]
        public string Assignee { get; set; }
        public List<SelectListItem> TicketStatuses { get; set; }
        public List<SelectListItem> TicketCategories { get; set; }
        public List<SelectListItem> TicketPriorities { get; set; }
        public List<SelectListItem> TicketAssignees { get; set; }
    }
}
