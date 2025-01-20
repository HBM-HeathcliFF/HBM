using HBM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Interfaces
{
    public interface IHbmDbContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Reaction> Reactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}