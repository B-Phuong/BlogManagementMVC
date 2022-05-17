using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {//
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                   .HasColumnType("VARCHAR(75)")
                   .HasMaxLength(75)
                   .IsRequired();
            builder.HasOne(p => p.Author)
                   .WithMany(a => a.Posts)
                   .HasForeignKey(p => p.AuthorId)
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.Property(s => s.AuthorId)
            //   .HasColumnType("BIGINT")
            //   .IsRequired();
            builder.HasIndex(p => p.AuthorId)
                   .HasName("idx_post_user");

            builder.HasMany(p => p.PostMetas)
                   .WithOne(pt => pt.Post)
                   .HasForeignKey(p => p.PostId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.MetaTitle)
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100);

            builder.Property(p => p.Slug)
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.HasIndex(p => p.Slug)
                   .IsUnique()
                   .HasName("uq_slug");

            builder.Property(p => p.Summary)
                   .HasColumnType("VARCHAR(255)")
                   .HasMaxLength(255);

            builder.Property(p => p.Content)
                   .HasColumnType("TEXT")
                   .HasDefaultValue(null);

            builder.HasMany(p => p.Posts)
                   .WithOne(a => a.PostParent)
                   .HasForeignKey(p => p.ParentID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.ParentID)
                    .HasColumnType("BIGINT")
                    .HasDefaultValue(null);
            builder.HasIndex(p => p.ParentID)
                   .HasName("idx_post_parent");


            builder.Property(p => p.Published)
                   .HasColumnType("TINYINT")
                   .IsRequired()
                   .HasDefaultValue(null);

            builder.Property(p => p.CreatedAt)
                   .HasColumnType("DATETIME")
                   .IsRequired();

            builder.Property(p => p.UpdatedAt)
                   .HasColumnType("DATETIME")
                   .HasDefaultValue(null);

            builder.Property(p => p.PublishedAt)
                   .HasColumnType("DATETIME")
                   .HasDefaultValue(null);

            builder.Property(p => p.TotalVote)
                   .HasDefaultValue("0");

            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue("false");

        }

    }
}
