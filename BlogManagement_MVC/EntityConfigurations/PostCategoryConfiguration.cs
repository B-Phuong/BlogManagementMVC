
using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class PostCategoryConfiguration: IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.HasKey(pc => new { pc.CategoryId, pc.PostId });
            builder.HasOne(pc => pc.Post)
                   .WithMany(s => s.PostCategories)
                   .HasForeignKey(pc=> pc.PostId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(pc => pc.Category)
                   .WithMany(s => s.PostCategories)
                   .HasForeignKey(pc => pc.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(pc => pc.CategoryId)
                   .HasName("idx_pc_category");
            builder.HasIndex(pc => pc.PostId)
                   .HasName("idx_pc_post");
        }
    }
}
