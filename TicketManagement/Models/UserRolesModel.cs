namespace TicketManagement.Models
{
    public class UserRolesModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
