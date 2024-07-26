using System.Collections.Generic;


namespace BusinessLogic.Entities
{
    public class TicketEntity
    {
        public int TicketID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PriorityID { get; set; }
        public CategoryEntity CategoryEntity { get; set; }
        public int StatusID { get; set; }
        public System.DateTime created_Date { get; set; }
        public System.DateTime audit_Date { get; set; }
        public string created_User { get; set; }
        public string audit_User { get; set; }
        public string Assignee { get; set; }
        public bool Deleted { get; set; }
        public ICollection<ImageEntity> Images { get; set; }
    }
}
