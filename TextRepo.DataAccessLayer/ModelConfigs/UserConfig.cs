using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.ModelConfigs
{
    /// <summary>
    /// Implements IEntityTypeConfiguration interface defining User configuration
    /// </summary>
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the entity of type User
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // many users to many projects
            builder
                .HasMany(p => p.Projects)
                .WithMany(u => u.Users);
            
            // indexes
            builder
                .HasIndex(u => u.Name);
            builder
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}