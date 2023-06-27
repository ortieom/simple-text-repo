using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.ModelConfigs
{
    /// <summary>
    /// Implements IEntityTypeConfiguration interface defining Project configuration
    /// </summary>
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        /// <summary>
        /// Configures the entity of type Project
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // many projects to many users
            builder
                .HasMany(p => p.Users)
                .WithMany(u => u.Projects)
                .UsingEntity<ProjectUser>();
            
            // one project to many documents
            builder
                .HasMany(p => p.Documents)
                .WithOne(p => p.Project)
                .HasForeignKey(b => b.ProjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            
            // lengths
            builder
                .Property(b => b.Name).HasMaxLength(50);
            builder
                .Property(b => b.Description).HasMaxLength(200);
        }
    }
}