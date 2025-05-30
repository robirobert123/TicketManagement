using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public class CommentEntity
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentUser { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string CommentUserID { get; set; }
        public int TicketID { get; set; }
    }
}
