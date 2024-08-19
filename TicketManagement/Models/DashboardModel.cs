using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketManagement.Models
{
    public class DashboardModel
    {
        public List<TicketDetailsModel> TicketDetails { get; set; }
        public List<StatusEntity> Statuses { get; set; }
        public List<SelectListItem> TicketPriorities { get; set; }
        public int PriorityID { get; set; }
    }
}
