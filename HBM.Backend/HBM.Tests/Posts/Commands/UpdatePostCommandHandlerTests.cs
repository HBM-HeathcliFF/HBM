using HBM.Application.Common.Exceptions;
using HBM.Application.Posts.Commands.UpdatePost;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Posts.Commands
{
    public class UpdatePostCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdatePostCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdatePostCommandHandler(Context);
            var updatedTitle = "new title";
            var updatedText = "new text";

            // Act
            await handler.Handle(new UpdatePostCommand
            {
                Id = HbmContextFactory.PostIdForUpdate,
                UserId = HbmContextFactory.UserBId,
                Title = updatedTitle,
                Text = updatedText
            },
            CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Posts.SingleOrDefaultAsync(post =>
                post.Id == HbmContextFactory.PostIdForUpdate &&
                post.Title == updatedTitle && post.Text == updatedText));
        }

        [Fact]
        public async Task UpdatePostCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdatePostCommandHandler(Context);
            var updatedTitle = "new title";
            var updatedText = "new text";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdatePostCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HbmContextFactory.UserBId,
                        Title = updatedTitle,
                        Text = updatedText
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePostCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdatePostCommandHandler(Context);
            var updatedTitle = "new title";
            var updatedText = "new text";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdatePostCommand
                    {
                        Id = HbmContextFactory.PostIdForUpdate,
                        UserId = HbmContextFactory.UserAId,
                        Title = updatedTitle,
                        Text = updatedText
                    },
                    CancellationToken.None));
        }
    }
}