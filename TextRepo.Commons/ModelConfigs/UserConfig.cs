using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextRepo.Commons.Models;

namespace TextRepo.Commons.ModelConfigs
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
            // indexes
            builder
                .HasIndex(u => u.Name);
            builder
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}