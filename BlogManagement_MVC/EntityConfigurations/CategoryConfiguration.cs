

using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title)
                   .HasColumnType("VARCHAR(75)")
                   .HasMaxLength(75) 
                   .IsRequired();

            builder.Property(c => c.MetaTitle)
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100)
                   .HasDefaultValue(null);

            builder.Property(c => c.Slug)
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.Content)
                   .HasColumnType("TEXT")
                   .HasDefaultValue(null);

            builder.HasMany(p => p.Categories)
                   .WithOne(a => a.CategoryParent)
                   .HasForeignKey(p => p.ParentID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.ParentID)
                   .HasColumnType("BIGINT")
                   .HasDefaultValue(null);

            builder.HasIndex(p => p.ParentID)
                   .HasName("idx_category_parent");

        }
    }
}
