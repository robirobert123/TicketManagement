using Microsoft.EntityFrameworkCore;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using TicketManagement.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TicketManagementDbContextConnection") ?? throw new InvalidOperationException("Connection string 'TicketManagementDbContextConnection' not found.");

builder.Services.AddDbContext<TicketManagementDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TicketManagementUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<TicketManagementDbContext>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
