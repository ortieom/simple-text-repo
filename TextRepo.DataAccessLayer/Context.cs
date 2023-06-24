using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextRepo.Commons.ModelConfigs;
using TextRepo.Commons.Models;
namespace TextRepo.DataAccessLayer
{
    public class Context : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<ContactInfo> Contacts => Set<ContactInfo>();

        private string DbPath { get; }
        private readonly bool _verbose;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Context(ILoggerFactory loggerFactory, IOptions<DbSettingsModel> options)
        {
            _loggerFactory = loggerFactory;
            DbPath = options.Value.ConnectionString;
            _verbose = options.Value.Verbose;
        }

        /// <summary>
        /// Configure context with chosen data provider
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            if (_verbose)
                optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        /// <summary>
        /// Fluent API configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ContactInfoConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new DocumentConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}