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
        public static async Task CreateUser(IServiceProvider serviceProvider, string userName, string password, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            var user = new User
            {
                UserName = userName,
                Email = $"{userName}@example.com",  // Example email address
                FirstName = userName,  // Assuming userName is also the first name
                LastName = "User"      // Default last name
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Ensure the role exists
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole<int> { Name = roleName };
                    await roleManager.CreateAsync(role);
                }

                // Assign the role to the user
                await userManager.AddToRoleAsync(user, role.Name);
            }
        }


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

            // Configure the relationships between User and its related entities
            modelBuilder.Entity<Practitioner>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Practitioner>(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete

            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete

            modelBuilder.Entity<BnI>()
                .HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<BnI>(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete

            // Configure the Appointment and TimeSlot relationship with explicit cascade behavior
            modelBuilder.Entity<Appointment>()
                .HasKey(a => new { a.PractitionerId, a.ClientId, a.DateId });

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Practitioner)
                .WithMany()
                .HasForeignKey(a => a.PractitionerId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.TimeSlot)
                .WithMany(t => t.Appointments)
                .HasForeignKey(a => a.DateId)
                .OnDelete(DeleteBehavior.NoAction);  // No cascading delete
        }


        //// Configure the model and relationships
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Seed Roles
        //    modelBuilder.Entity<IdentityRole<int>>().HasData(
        //        new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
        //        new IdentityRole<int> { Id = 2, Name = "Practitioner", NormalizedName = "PRACTITIONER" },
        //        new IdentityRole<int> { Id = 3, Name = "Client", NormalizedName = "CLIENT" },
        //        new IdentityRole<int> { Id = 4, Name = "Billing", NormalizedName = "BILLING" }
        //    );

        //    //// Seed Users with static Ids
        //    //modelBuilder.Entity<User>().HasData(
        //    //    new User
        //    //    {
        //    //        Id = 1,
        //    //        UserName = "admin@example.com",
        //    //        NormalizedUserName = "ADMIN@EXAMPLE.COM",
        //    //        Email = "admin@example.com",
        //    //        NormalizedEmail = "ADMIN@EXAMPLE.COM",
        //    //        FirstName = "Justin",
        //    //        LastName = "Schulz"
        //    //    },
        //    //    new User
        //    //    {
        //    //        Id = 2,
        //    //        UserName = "doc@example.com",
        //    //        NormalizedUserName = "DOC@EXAMPLE.COM",
        //    //        Email = "doc@example.com",
        //    //        NormalizedEmail = "DOC@EXAMPLE.COM",
        //    //        FirstName = "Doctor",
        //    //        LastName = "Smith"
        //    //    },
        //    //    new User
        //    //    {
        //    //        Id = 3,
        //    //        UserName = "billing@example.com",
        //    //        NormalizedUserName = "BILLING@EXAMPLE.COM",
        //    //        Email = "billing@example.com",
        //    //        NormalizedEmail = "BILLING@EXAMPLE.COM",
        //    //        FirstName = "Bill",
        //    //        LastName = "Johnson"
        //    //    },
        //    //    new User
        //    //    {
        //    //        Id = 4,
        //    //        UserName = "client@example.com",
        //    //        NormalizedUserName = "CLIENT@EXAMPLE.COM",
        //    //        Email = "client@example.com",
        //    //        NormalizedEmail = "CLIENT@EXAMPLE.COM",
        //    //        FirstName = "Client",
        //    //        LastName = "User"
        //    //    }
        //    //);

        //    //// Seed Practitioners, Clients, Admins, and BnI with static UserIds
        //    //modelBuilder.Entity<Practitioner>().HasData(
        //    //    new Practitioner { PractitionerId = 1, UserId = 2 }  // Practitioner UserId is 2 (from the User table)
        //    //);

        //    //modelBuilder.Entity<Client>().HasData(
        //    //    new Client { UserId = 3 }  // Client UserId is 3 (from the User table)
        //    //);

        //    //modelBuilder.Entity<Admin>().HasData(
        //    //    new Admin { UserId = 1 }  // Admin UserId is 1 (from the User table)
        //    //);

        //    //modelBuilder.Entity<BnI>().HasData(
        //    //    new BnI { UserId = 4 }  // BnI UserId is 4 (from the User table)
        //    //);

        //    // Configure the relationships between User and its related entities
        //    modelBuilder.Entity<Practitioner>()
        //        .HasOne(p => p.User)
        //        .WithOne()
        //        .HasForeignKey<Practitioner>(p => p.UserId);

        //    modelBuilder.Entity<Client>()
        //        .HasOne(c => c.User)
        //        .WithOne()
        //        .HasForeignKey<Client>(c => c.UserId);

        //    modelBuilder.Entity<Admin>()
        //        .HasOne(a => a.User)
        //        .WithOne()
        //        .HasForeignKey<Admin>(a => a.UserId);

        //    modelBuilder.Entity<BnI>()
        //        .HasOne(b => b.User)
        //        .WithOne()
        //        .HasForeignKey<BnI>(b => b.UserId);

        //    // Composite Key for Appointment
        //    modelBuilder.Entity<Appointment>()
        //        .HasKey(a => new { a.PractitionerId, a.ClientId, a.DateId });
        //}
    }
}
