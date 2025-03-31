using EnterpriseProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//configure identity services first
builder.Services.AddIdentity<User, IdentityRole>(options => {
    //options.Password.RequiredLength = 6;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequireDigit = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var connStr = builder.Configuration.GetConnectionString("ProjectDb");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connStr));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();  //enable authentication
app.UseAuthorization();   //enable authorization

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// add admin user after services are configured
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var transactionContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await ApplicationDbContext.CreateAdminUser(scope.ServiceProvider);  //create admin user with correct scopes
}

app.Run();
