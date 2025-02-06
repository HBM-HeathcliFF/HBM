using HBM.Application.Common.Exceptions;
using HBM.Application.Posts.Commands.CreatePost;
using HBM.Application.Posts.Commands.DeletePost;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Posts.Commands
{
    public class DeletePostCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeletePostCommandHandler_Success()
        {
            // Arrange
            var handler = new DeletePostCommandHandler(Context);

            // Act
            await handler.Handle(new DeletePostCommand
            {
                Id = HbmContextFactory.PostIdForDelete,
                UserId = HbmContextFactory.UserAId
            },
            CancellationToken.None);

            // Assert
            Assert.Null(await Context.Posts.SingleOrDefaultAsync(post =>
                post.Id == HbmContextFactory.PostIdForDelete));
        }

        [Fact]
        public async Task DeletePostCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeletePostCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeletePostCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HbmContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeletePostCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeletePostCommandHandler(Context);
            var createHandler = new CreatePostCommandHandler(Context);
            var postId = await createHandler.Handle(
                new CreatePostCommand
                {
                    Title = "PostTitle",
                    Text = "PostText",
                    UserId = HbmContextFactory.UserAId
                },
                CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeletePostCommand
                    {
                        Id = postId,
                        UserId = HbmContextFactory.UserBId
                    },
                    CancellationToken.None));
        }
    }
}
