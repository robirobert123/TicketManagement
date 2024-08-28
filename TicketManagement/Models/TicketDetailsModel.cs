using BusinessLogic.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace TicketManagement.Models
{
    public class TicketDetailsModel
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
        public System.DateTime CreatedDate { get; set; }
        [DisplayName("Last update:")]
        public System.DateTime AuditDate { get; set; }
        [DisplayName("Created by:")]
        public string CreatedUser { get; set; }
        [DisplayName("Last updated by:")]
        public string AuditUser { get; set; }
        public string Assignee { get; set; }
        [DisplayName("Assignee:")]
        public string AssigneeName { get; set; }
        //public ICollection<ImageModel> Images { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}
