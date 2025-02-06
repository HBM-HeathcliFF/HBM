using HBM.Domain;
using HBM.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Common
{
    public class HbmContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid PostIdForDelete = Guid.NewGuid();
        public static Guid PostIdForUpdate = Guid.NewGuid();

        public static Guid CommentIdForDelete = Guid.NewGuid();
        public static Guid CommentIdForUpdate = Guid.NewGuid();

        public static Guid ReactionIdForDelete = Guid.NewGuid();

        public static HbmDbContext Create()
        {
            var options = new DbContextOptionsBuilder<HbmDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new HbmDbContext(options);
            context.Database.EnsureCreated();
            context.AddRange(
                new AppUser
                {
                    Id = UserAId,
                    Role = "Admin",
                    UserName = "UserA"
                },
                new AppUser
                {
                    Id = UserBId,
                    Role = "User",
                    UserName = "UserB"
                },

                new Post
                {
                    CreationDate = DateTime.Today.ToString(),
                    Text = "Text1",
                    EditDate = null,
                    Id = Guid.Parse("4F8F241A-93D2-41B1-835A-6DE5207F8E9F"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Post
                {
                    CreationDate = DateTime.Today.ToString(),
                    Text = "Text2",
                    EditDate = null,
                    Id = Guid.Parse("84E2CD4A-3A52-42C0-B903-BCDD9B215114"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Post
                {
                    CreationDate = DateTime.Today.ToString(),
                    Text = "Text3",
                    EditDate = null,
                    Id = PostIdForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Post
                {
                    CreationDate = DateTime.Today.ToString(),
                    Text = "Text4",
                    EditDate = null,
                    Id = PostIdForUpdate,
                    Title = "Title4",
                    UserId = UserBId
                },

                new Comment
                {
                    Id = Guid.Parse("F22E36A9-474B-40DB-BF74-EC85553C0EFF"),
                    Text = "Text1",
                    CreationDate = DateTime.Today.ToString(),
                    EditDate = null,
                    UserId = UserAId,
                    PostId = PostIdForDelete,
                },
                new Comment
                {
                    Id = Guid.Parse("8883462E-89DA-49FC-B05A-17210951EE98"),
                    Text = "Text2",
                    CreationDate = DateTime.Today.ToString(),
                    EditDate = null,
                    UserId = UserBId,
                    PostId = PostIdForUpdate,
                },
                new Comment
                {
                    Id = CommentIdForDelete,
                    Text = "Text3",
                    CreationDate = DateTime.Today.ToString(),
                    EditDate = null,
                    UserId = UserAId,
                    PostId = PostIdForDelete,
                },
                new Comment
                {
                    Id = CommentIdForUpdate,
                    Text = "Text4",
                    CreationDate = DateTime.Today.ToString(),
                    EditDate = null,
                    UserId = UserBId,
                    PostId = PostIdForUpdate,
                },

                new Reaction
                {
                    Id = Guid.Parse("26391749-A480-4AF8-AC41-343FF27BAE30"),
                    UserId = UserAId,
                    PostId = PostIdForUpdate,
                },
                new Reaction
                {
                    Id = Guid.Parse("08ADA1EA-6F2A-4BA8-9F5F-1832F9D39FE9"),
                    UserId = UserAId,
                    PostId = PostIdForDelete,
                },
                new Reaction
                {
                    Id = ReactionIdForDelete,
                    UserId = UserBId,
                    PostId = PostIdForDelete,
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(HbmDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}