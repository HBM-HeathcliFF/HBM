using HBM.Application.Interfaces;
using HBM.Domain;
using HBM.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace HBM.Persistence
{
    public class HbmDbContext : DbContext, IHbmDbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

        public HbmDbContext(DbContextOptions<HbmDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new ReactionConfiguration());
            base.OnModelCreating(builder);
        }
    }
}