using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Models
{
    public class TicketModel
    {
        [Key]
        public int TicketID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PriorityID { get; set; }
        public string PriorityText { get; set; }
        public int CategoryID { get; set; }
        [DisplayName("Category")]
        public string CategoryText { get; set; }
        public int StatusID { get; set; }
        public string StatusText { get; set; }
        public System.DateTime created_Date { get; set; }
        public System.DateTime audit_Date { get; set; }
        public string created_User { get; set; }
        public string audit_User { get; set; }
        public string Assignee { get; set; }
        public string AssigneeName { get; set; }
        //public ICollection<ImageModel> Images { get; set; }
    }
}
