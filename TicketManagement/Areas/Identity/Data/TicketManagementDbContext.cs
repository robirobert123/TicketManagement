using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Models;

namespace TicketManagement.Data;

public class TicketManagementDbContext : IdentityDbContext<TicketManagementUser>
{
    public TicketManagementDbContext(DbContextOptions<TicketManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<TicketManagement.Models.TicketModel> TicketModel { get; set; } = default!;
}
