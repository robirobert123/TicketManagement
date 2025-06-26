using DataAcces;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Data;
using TicketManagement.Helpers;
using TicketManagement.Services;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TicketManagementDbContextConnection") ?? throw new InvalidOperationException("Connection string 'TicketManagementDbContextConnection' not found.");

builder.Services.AddDbContext<TicketManagementDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<TicketManagementUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<TicketManagementDbContext>();

// Register your repositories for DI
builder.Services.AddScoped<TicketManagementEntities>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddSingleton<OpenAIService>();
builder.Services.AddScoped<TicketHelper>();

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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

// Custom route for user dashboard
app.MapControllerRoute(
    name: "userDashboard",
    pattern: "dashboard",
    defaults: new { controller = "UserDashboard", action = "Index" });
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User", "Developer" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TicketManagementUser>>();
    string email = "admin@admin.com";
    string password = "Test.1234";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new TicketManagementUser
        {
            FirstName = "Admin",
            LastName = "Admin",
            UserName = email,
            Email = email,
        };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}



app.Run();
