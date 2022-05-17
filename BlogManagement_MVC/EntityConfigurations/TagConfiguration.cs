using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .HasColumnType("VARCHAR(75)")
                   .HasMaxLength(75);
        
            builder.Property(t => t.MetaTitle)
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100);

            builder.Property(t => t.Slug)
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100);

            builder.Property(t => t.Content)
                   .HasColumnType("TEXT");
        }
    }
}
