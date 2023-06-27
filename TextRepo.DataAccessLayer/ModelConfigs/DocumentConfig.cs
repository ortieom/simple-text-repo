using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TextRepo.Commons.Models;

namespace TextRepo.Commons.ModelConfigs
{
    /// <summary>
    /// Implements IEntityTypeConfiguration interface defining Document configuration
    /// </summary>
    public class DocumentConfig : IEntityTypeConfiguration<Document>
    {
        /// <summary>
        /// Configures the entity of type Document
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(b => b.Title).HasMaxLength(50);
            builder.Property(b => b.Description).HasMaxLength(200);
        }
    }
}