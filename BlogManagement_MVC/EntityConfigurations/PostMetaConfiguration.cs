using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class PostMetaConfiguration:IEntityTypeConfiguration<PostMeta>

    {
        public void Configure(EntityTypeBuilder<PostMeta> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.PostId)
                   .IsRequired();

            builder.HasIndex(pm =>new { pm.PostId, pm.Key })
                   .HasName("idx_meta_post");

            builder.Property(pm => pm.Key)
                   .IsRequired()
                   .HasColumnType("VARCHAR(50)")
                   .HasMaxLength(50)
                   .HasDefaultValue(null);

            builder.Property(pm => pm.Content)
                   .HasColumnType("TEXT")
                   .HasDefaultValue(null);

            builder.HasOne(pm => pm.Post)
                   .WithMany(p => p.PostMetas)
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
