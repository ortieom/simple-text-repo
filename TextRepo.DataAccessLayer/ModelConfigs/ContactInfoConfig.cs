using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextRepo.Commons.Models;

namespace TextRepo.Commons.ModelConfigs
{
    /// <summary>
    /// Implements IEntityTypeConfiguration interface defining ContactInfo configuration
    /// </summary>
    public class ContactInfoConfig : IEntityTypeConfiguration<ContactInfo>
    {
        /// <summary>
        /// Configures the entity of type ContactInfo
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            // one user to zero-one contacts 
            builder
                .HasOne(u => u.User)
                .WithOne(ci => ci.ContactInfo)
                .HasForeignKey<ContactInfo>(u => u.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            
            // lengths
            builder
                .Property(b => b.Type).HasMaxLength(50);
            builder
                .Property(b => b.Value).HasMaxLength(200);

        }
    }
}