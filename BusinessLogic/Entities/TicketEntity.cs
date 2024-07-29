using System.Collections.Generic;


namespace BusinessLogic.Entities
{
    public class TicketEntity
    {
        public int TicketID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PriorityEntity PriorityEntity { get; set; }
        public CategoryEntity CategoryEntity { get; set; }
        public StatusEntity StatusEntity { get; set; }
        public System.DateTime created_Date { get; set; }
        public System.DateTime audit_Date { get; set; }
        public string created_User { get; set; }
        public string audit_User { get; set; }
        public UserEntity AssigneeEntity { get; set; }
        public bool Deleted { get; set; }
        public ICollection<ImageEntity> Images { get; set; }
    }
}
