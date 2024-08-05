using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
