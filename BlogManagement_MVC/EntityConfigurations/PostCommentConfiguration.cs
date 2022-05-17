using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>

    {//
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Title)
                   .HasColumnType("VARCHAR(100)") 
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(pm => pm.Published)
                   .HasColumnType("TINYINT")
                   .HasMaxLength(1)
                   .IsRequired() 
                   .HasDefaultValue(null);

            builder.Property(pm => pm.Content)
                   .HasColumnType("TEXT")
                   .HasDefaultValue(null);

            builder.HasOne(pm => pm.Post)
                   .WithMany(p => p.PostComments)
                   .HasForeignKey(pm => pm.PostId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(pm => pm.PostId)
                    .HasColumnType("BIGINT")
                    .IsRequired();

            builder.HasIndex(pm => pm.PostId)
                   .HasName("idx_comment_post");

            builder.Property(pm => pm.CreatedAt)
                   .HasColumnType("DATETIME")
                   .IsRequired();

            builder.Property(pm => pm.PublishedAt)
                   .HasColumnType("DATETIME")
                   .HasDefaultValue(null);

            builder.HasOne(pm => pm.PostParent)
                   .WithMany(p => p.Posts)
                   .HasForeignKey(pm => pm.ParentID);

            builder.Property(pm => pm.ParentID)
                   .HasColumnType("BIGINT")
                   .HasDefaultValue(null);

            builder.HasIndex(pm => pm.ParentID)
                   .HasName("idx_comment_parent");


            builder.HasOne(pm => pm.AuthorComment)
                   .WithMany(p => p.PostComments)
                   .HasForeignKey(pm => pm.AuthorCommentId);

        }
    }
}
