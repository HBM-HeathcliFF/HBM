using HBM.Application.Comments.Commands.CreateComment;
using HBM.Application.Comments.Commands.DeleteComment;
using HBM.Application.Common.Exceptions;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Comments.Commands
{
    public class DeleteCommentCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteCommentCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteCommentCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteCommentCommand
            {
                Id = HbmContextFactory.CommentIdForDelete,
                UserId = HbmContextFactory.UserAId,
                PostId = HbmContextFactory.PostIdForDelete
            },
            CancellationToken.None);

            // Assert
            Assert.Null(await Context.Comments.SingleOrDefaultAsync(comment =>
                comment.Id == HbmContextFactory.CommentIdForDelete));
        }

        [Fact]
        public async Task DeleteCommentCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteCommentCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCommentCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HbmContextFactory.UserAId,
                        PostId = HbmContextFactory.PostIdForDelete
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCommentCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteCommentCommandHandler(Context);
            var createHandler = new CreateCommentCommandHandler(Context);
            var commentId = await createHandler.Handle(
                new CreateCommentCommand
                {
                    Text = "PostText",
                    UserId = HbmContextFactory.UserAId,
                    PostId = HbmContextFactory.PostIdForDelete
                },
                CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteCommentCommand
                    {
                        Id = commentId,
                        UserId = HbmContextFactory.UserBId,
                        PostId = HbmContextFactory.PostIdForDelete
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCommentCommandHandler_FailOnWrongPostId()
        {
            // Arrange
            var deleteHandler = new DeleteCommentCommandHandler(Context);
            var createHandler = new CreateCommentCommandHandler(Context);
            var commentId = await createHandler.Handle(
                new CreateCommentCommand
                {
                    Text = "PostText",
                    UserId = HbmContextFactory.UserAId,
                    PostId = HbmContextFactory.PostIdForDelete
                },
                CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteCommentCommand
                    {
                        Id = commentId,
                        UserId = HbmContextFactory.UserAId,
                        PostId = Guid.NewGuid()
                    },
                    CancellationToken.None));
        }
    }
}