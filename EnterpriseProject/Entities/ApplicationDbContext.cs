using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseProject.Entities
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        //Let's connect our Entities to the Database class
        public DbSet<Appointment> Appointments { get; set; }






        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "password";
            string roleName = "Admin";

            // If role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // If username doesn't exist, create it and add it to the role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Admin",    // Set the FirstName
                    LastName = "User",      // Set the LastName
                    Email = "admin@example.com"  // You should also set Email as Identity requires it
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    // Handle the failure if needed
                }
            }
        }


        public static async Task CreatePractitionerUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "doc";
            string password = "password";
            string roleName = "Practitioner";

            // If role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // If username doesn't exist, create it and add it to the role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Doctor",    // Set FirstName
                    LastName = "Smith",      // Set LastName
                    Email = "doc@example.com" // Set Email
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        public static async Task CreateBillingUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "billing";
            string password = "password";
            string roleName = "Billing";

            // If role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // If username doesn't exist, create it and add it to the role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Bill",     // Set FirstName
                    LastName = "Johnson",   // Set LastName
                    Email = "billing@example.com" // Set Email
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
        public static async Task CreateClientUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "client";
            string password = "password";
            string roleName = "Client";

            // If role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // If username doesn't exist, create it and add it to the role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Client",   // Set FirstName
                    LastName = "User",      // Set LastName
                    Email = "client@example.com" // Set Email
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasKey(a => new { a.PractitionerId, a.ClientId, a.DateId });
        }
    }
}
