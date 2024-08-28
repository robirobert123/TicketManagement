using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Areas.Identity.Data;

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

    public DbSet<TicketManagement.Models.TicketDetailsModel> TicketModel { get; set; } = default!;

    public DbSet<TicketManagement.Models.TicketEditModel> TicketEditModel { get; set; } = default!;
}
