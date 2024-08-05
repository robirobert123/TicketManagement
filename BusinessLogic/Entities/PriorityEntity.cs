using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public class PriorityEntity
    {
        [Key]
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
    }
}
