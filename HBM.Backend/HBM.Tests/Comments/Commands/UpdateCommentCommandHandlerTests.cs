using HBM.Application.Comments.Commands.UpdateComment;
using HBM.Application.Common.Exceptions;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Comments.Commands
{
    public class UpdateCommentCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateCommentCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateCommentCommandHandler(Context);
            var updatedText = "new text";

            // Act
            await handler.Handle(new UpdateCommentCommand
            {
                Id = HbmContextFactory.CommentIdForUpdate,
                UserId = HbmContextFactory.UserBId,
                PostId = HbmContextFactory.PostIdForUpdate,
                Text = updatedText
            },
            CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Comments.SingleOrDefaultAsync(comment =>
                comment.Id == HbmContextFactory.CommentIdForUpdate &&
                comment.Text == updatedText));
        }

        [Fact]
        public async Task UpdateCommentCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateCommentCommandHandler(Context);
            var updatedText = "new text";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCommentCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HbmContextFactory.UserBId,
                        PostId = HbmContextFactory.PostIdForUpdate,
                        Text = updatedText
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCommentCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateCommentCommandHandler(Context);
            var updatedText = "new text";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCommentCommand
                    {
                        Id = HbmContextFactory.CommentIdForUpdate,
                        UserId = HbmContextFactory.UserAId,
                        PostId = HbmContextFactory.PostIdForUpdate,
                        Text = updatedText
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCommentCommandHandler_FailOnWrongPostId()
        {
            // Arrange
            var handler = new UpdateCommentCommandHandler(Context);
            var updatedText = "new text";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCommentCommand
                    {
                        Id = HbmContextFactory.CommentIdForUpdate,
                        UserId = HbmContextFactory.UserBId,
                        PostId = Guid.NewGuid(),
                        Text = updatedText
                    },
                    CancellationToken.None));
        }
    }
}