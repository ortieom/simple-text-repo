using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextRepo.Commons.Models;
namespace TextRepo.DataAccessLayer.ModelConfigs
{
    /// <summary>
    /// Implements IEntityTypeConfiguration interface defining ProjectUser (joining entity) configuration
    /// </summary>
    public class ProjectUserConfig : IEntityTypeConfiguration<ProjectUser>
    {
        /// <summary>
        /// Configures the entity of type ProjectUser
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ProjectUser> builder)
        {
            // unique pair
            builder.HasKey(pu => new { pu.UsersId, pu.ProjectsId });

            // one user to many joining entities
            builder
                .HasOne<User>(pu => pu.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.UsersId);

            // one project to many joining entities
            builder
                .HasOne<Project>(pu => pu.Project)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectsId);
        }
    }
}