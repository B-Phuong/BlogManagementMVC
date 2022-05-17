using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement_MVC.EntityConfigurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>

    { //
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.ToTable("User");
            builder.HasKey(s => s.Id);

            //builder.Property(u => u.Id)
            //    .HasColumnType("BIGINT");

            builder.Property(u => u.FirstName)
                   .HasColumnType("VARCHAR(50)")
                   .HasMaxLength(50)
                   .HasDefaultValue(null);

            builder.Property(u => u.LastName)
                   .HasColumnType("VARCHAR(50)")
                   .HasMaxLength(50)
                   .HasDefaultValue(null);

            builder.Property(u => u.PhoneNumber)
                   .HasColumnType("VARCHAR(15)")
                   .HasMaxLength(15);

            builder.HasIndex(b => b.PhoneNumber)
                   .IsUnique()
                   .HasName("uq_mobile");


            builder.Property(u => u.Email)
                   .HasColumnType("VARCHAR(50)")
                   .HasMaxLength(50);

            builder.HasIndex(b => b.Email)
                   .IsUnique()
                   .HasName("uq_email");

            builder.Property(u => u.PasswordHash)
                   .HasColumnType("VARCHAR(255)") //32
                   .HasMaxLength(255) //32
                   .IsRequired();

            builder.Property(u => u.RegisteredAt)
                   .HasColumnType("DATETIME")
                   .IsRequired();

            builder.Property(u => u.LastLogIn)
                   .HasColumnType("DATETIME")
                   .HasDefaultValue(null);

            builder.HasMany(s => s.Posts)
                   .WithOne(a => a.Author)
                   .HasForeignKey(s => s.AuthorId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(u => u.Intro)
                   .HasColumnType("VARCHAR(255)")
                   .HasMaxLength(255)
                   .HasDefaultValueSql(null);

            builder.Property(u => u.Profile)
                   .HasColumnType("TEXT")
                   .HasDefaultValue(null);
        }
    }
}
