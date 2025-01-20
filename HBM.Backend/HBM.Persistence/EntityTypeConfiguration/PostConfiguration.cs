using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HBM.Domain;

namespace HBM.Persistence.EntityTypeConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(post => post.Id);
            builder.HasIndex(post => post.Id).IsUnique();
            builder.Property(post => post.Title).HasMaxLength(250);
            builder
                .HasOne(post => post.AppUser)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.UserId);
        }
    }
}