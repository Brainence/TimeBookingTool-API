using System.Data.Entity;
using System.Configuration;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
        }

        public DataContext()
            : this(ConnectionString)
        {
        }

        public static string ConnectionString
        {
            get
            {
                var temp = ConfigurationManager.ConnectionStrings["TimeBookingToolConnectionString"];
                if (temp != null)
                {
                    return ConfigurationManager.ConnectionStrings["TimeBookingToolConnectionString"].ConnectionString;
                }
                return "TimeBookingToolConnectionString";
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Activity> Tasks { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<ResetTicket> ResetTickets { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Projects)
                .WithRequired(p => p.Customer)
                .HasForeignKey(p => p.CustomerId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TimeEntry>()
                .HasRequired(u => u.User)
                .WithMany(p => p.TimeEntries)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Activity>()
                .HasRequired(u => u.Project)
                .WithMany(p => p.Activities)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(u => u.Users)
                .WithMany(p => p.Projects)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProjectId");
                    cs.MapRightKey("UserId");
                    cs.ToTable("UserProject");
                });

            modelBuilder.Entity<Project>()
                .HasMany(u => u.Activities)
                .WithRequired(p => p.Project)
                .HasForeignKey(p => p.ProjectId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Project>()
                .HasRequired(u => u.Customer)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("ProjectId");
                    cs.ToTable("UserProject");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.TimeEntries)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Company>()
                .HasMany(u => u.Users)
                .WithRequired(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Company>()
                .HasMany(u => u.Projects)
                .WithRequired(p => p.Company)
                .HasForeignKey(p => p.CompanyId);

            modelBuilder.Entity<Company>()
                .HasMany(u => u.Customers)
                .WithRequired(p => p.Company)
                .HasForeignKey(p => p.CompanyId);
        }
    }
}
