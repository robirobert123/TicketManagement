using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public class StatusEntity
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
