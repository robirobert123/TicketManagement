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
        [DisplayName("Priority")]
        public string PriorityText { get; set; }
        public int CategoryID { get; set; }
        [DisplayName("Category")]
        public string CategoryText { get; set; }
        public int StatusID { get; set; }
        [DisplayName("Status")]
        public string StatusText { get; set; }
        [DisplayName("Created at:")]
        public System.DateTime created_Date { get; set; }
        [DisplayName("Last update:")]
        public System.DateTime audit_Date { get; set; }
        [DisplayName("Created by:")]
        public string created_User { get; set; }
        [DisplayName("Last updated by:")]
        public string audit_User { get; set; }
        public string Assignee { get; set; }
        [DisplayName("Assignee:")]
        public string AssigneeName { get; set; }
        //public ICollection<ImageModel> Images { get; set; }
    }
}
