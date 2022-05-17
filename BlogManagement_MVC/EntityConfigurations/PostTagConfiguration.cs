
using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class PostTagConfiguration: IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(pt => new { pt.TagId, pt.PostId });
            builder.HasOne(pt => pt.Tag)
                   .WithMany(s => s.PostTags)
                   .HasForeignKey(pt => pt.TagId);


            builder.HasOne(pt => pt.Post)
                   .WithMany(s => s.PostTags)
                   .HasForeignKey(pt => pt.PostId);

        }
    }
}
