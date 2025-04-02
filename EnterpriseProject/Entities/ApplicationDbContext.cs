using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnterpriseProject.Entities
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        // Connect Entities to the Database
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<BnI> BnIs { get; set; }  // Fixed typo in DbSet naming
        public DbSet<Admin> Admins { get; set; }

        // Methods to create users with roles
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "password";
            string roleName = "Admin";

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com"
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        public static async Task CreatePractitionerUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "doc";
            string password = "password";
            string roleName = "Practitioner";

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Doctor",
                    LastName = "Smith",
                    Email = "doc@example.com"
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);

                    // Create Practitioner entity
                    var practitioner = new Practitioner
                    {
                        UserId = user.Id // UserId is now a string
                    };
                    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Practitioners.Add(practitioner);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public static async Task CreateBillingUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "billing";
            string password = "password";
            string roleName = "Billing";

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Bill",
                    LastName = "Johnson",
                    Email = "billing@example.com"
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
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "client";
            string password = "password";
            string roleName = "Client";

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User
                {
                    UserName = username,
                    FirstName = "Client",
                    LastName = "User",
                    Email = "client@example.com"
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        // Configure the model and relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Practitioner", NormalizedName = "PRACTITIONER" },
                new IdentityRole<int> { Id = 3, Name = "Client", NormalizedName = "CLIENT" },
                new IdentityRole<int> { Id = 4, Name = "Billing", NormalizedName = "BILLING" }
            );

            // Seed Users with static Ids
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    FirstName = "Justin",
                    LastName = "Schulz"
                },
                new User
                {
                    Id = 2,
                    UserName = "doc@example.com",
                    NormalizedUserName = "DOC@EXAMPLE.COM",
                    Email = "doc@example.com",
                    NormalizedEmail = "DOC@EXAMPLE.COM",
                    FirstName = "Doctor",
                    LastName = "Smith"
                },
                new User
                {
                    Id = 3,
                    UserName = "billing@example.com",
                    NormalizedUserName = "BILLING@EXAMPLE.COM",
                    Email = "billing@example.com",
                    NormalizedEmail = "BILLING@EXAMPLE.COM",
                    FirstName = "Bill",
                    LastName = "Johnson"
                },
                new User
                {
                    Id = 4,
                    UserName = "client@example.com",
                    NormalizedUserName = "CLIENT@EXAMPLE.COM",
                    Email = "client@example.com",
                    NormalizedEmail = "CLIENT@EXAMPLE.COM",
                    FirstName = "Client",
                    LastName = "User"
                }
            );

            // Seed Practitioners, Clients, Admins, and BnI with static UserIds
            modelBuilder.Entity<Practitioner>().HasData(
                new Practitioner { PractitionerId = 1, UserId = 2 }  // Practitioner UserId is 2 (from the User table)
            );

            //modelBuilder.Entity<Client>().HasData(
            //    new Client { UserId = 3 }  // Client UserId is 3 (from the User table)
            //);

            //modelBuilder.Entity<Admin>().HasData(
            //    new Admin { UserId = 1 }  // Admin UserId is 1 (from the User table)
            //);

            //modelBuilder.Entity<BnI>().HasData(
            //    new BnI { UserId = 4 }  // BnI UserId is 4 (from the User table)
            //);

            // Configure the relationships between User and its related entities
            modelBuilder.Entity<Practitioner>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Practitioner>(p => p.UserId);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a => a.UserId);

            modelBuilder.Entity<BnI>()
                .HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<BnI>(b => b.UserId);

            // Composite Key for Appointment
            modelBuilder.Entity<Appointment>()
                .HasKey(a => new { a.PractitionerId, a.ClientId, a.DateId });
        }
    }
}
