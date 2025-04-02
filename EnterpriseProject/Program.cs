using EnterpriseProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure identity services
builder.Services.AddIdentity<User, IdentityRole<int>>(options => 
{
    // Allow simpler passwords
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;  // Ensure at least 6 characters
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Set up database connection
var connStr = builder.Configuration.GetConnectionString("ProjectDb");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Create Admin user after services are configured
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

    // Ensure the database is created and migrations are applied
    dbContext.Database.Migrate();

    // Create the Admin user
    await ApplicationDbContext.CreateAdminUser(serviceProvider);
    await ApplicationDbContext.CreatePractitionerUser(serviceProvider);
    await ApplicationDbContext.CreateBillingUser(serviceProvider);
    await ApplicationDbContext.CreateClientUser(serviceProvider);
}

app.Run();
