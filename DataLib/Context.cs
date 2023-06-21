using DataLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLib {
    public class Context : DbContext {
        private readonly ILogger? _logger;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ContactInfo> Contacts { get; set; } = null!;

        private string DbPath { get; }

        /// <summary>
        /// Default constructor
        /// TODO: load dataSource from config!
        /// </summary>
        public Context() {
            DbPath = "storage.db";
        }

        public Context(ILogger<Context> logger) {
            _logger = logger;
            DbPath = "D:\\Projects\\dotnet-test\\ConsoleApp\\ConsoleApp\\storage.db";
        }

        /// <summary>
        /// Configure context with chosen data provider
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            _logger?.LogInformation("Using database at {0}", DbPath);
        }

        /// <summary>
        /// Fluent API configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // primary keys
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<Project>().HasKey(t => t.Id);
            modelBuilder.Entity<Document>().HasKey(t => t.Id);
            modelBuilder.Entity<ContactInfo>().HasKey(t => t.Id);

            // one to zero or one 
            modelBuilder.Entity<ContactInfo>()
                .HasOne(u => u.User)
                .WithOne(ci => ci.ContactInfo)
                .HasForeignKey<ContactInfo>(u => u.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // one to many
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Documents)
                .WithOne(p => p.Project)
                .HasForeignKey(b => b.ProjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // many to many
            modelBuilder.Entity<User>()
                .HasMany(p => p.Projects)
                .WithMany(u => u.Users);

            // constraints
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => new { u.Email });  // unique
            // length
            modelBuilder.Entity<User>()
                .Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<Project>()
                .Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<Project>()
                .Property(b => b.Description).HasMaxLength(200);
            modelBuilder.Entity<Document>()
                .Property(b => b.Title).HasMaxLength(50);
            modelBuilder.Entity<Document>()
                .Property(b => b.Description).HasMaxLength(200);
            modelBuilder.Entity<ContactInfo>()
                .Property(b => b.Type).HasMaxLength(50);
            modelBuilder.Entity<ContactInfo>()
                .Property(b => b.Value).HasMaxLength(200);

            // indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email);

            base.OnModelCreating(modelBuilder);

            _logger?.LogDebug("Db configured");
        }
    }
}