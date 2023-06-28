using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TextRepo.DataAccessLayer.ModelConfigs;
using TextRepo.Commons.Models;
namespace TextRepo.DataAccessLayer
{
    /// <summary>
    /// Represents a configurable session with the database
    /// </summary>
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        /// <summary>
        /// A set that is used to query and save User instances  
        /// </summary>
        public DbSet<User> Users => Set<User>();
        /// <summary>
        /// A set that is used to query and save Projects instances  
        /// </summary>
        public DbSet<Project> Projects => Set<Project>();
        /// <summary>
        /// A set that is used to query and save Documents instances  
        /// </summary>
        public DbSet<Document> Documents => Set<Document>();
        /// <summary>
        /// A set that is used to query and save Contacts instances  
        /// </summary>
        public DbSet<ContactInfo> Contacts => Set<ContactInfo>();
        /// <summary>
        /// A set that is used to query and save ProjectUser joining instances  
        /// </summary>
        public DbSet<ProjectUser> ProjectUser => Set<ProjectUser>();

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
// ExplicitProjectUserConnection