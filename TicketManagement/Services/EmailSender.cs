using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace TicketManagement.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // For development, just log the email or implement actual email sending
            // using SendGrid, SMTP, etc.
            return Task.CompletedTask;
        }
    }
}
