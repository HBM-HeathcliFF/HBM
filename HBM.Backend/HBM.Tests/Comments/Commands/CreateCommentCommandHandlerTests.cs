using HBM.Application.Comments.Commands.CreateComment;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Comments.Commands
{
    public class CreateCommentCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateCommentCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateCommentCommandHandler(Context);
            var text = "comment text";

            // Act
            var commentId = await handler.Handle(
                new CreateCommentCommand
                {
                    Text = text,
                    PostId = HbmContextFactory.PostIdForDelete,
                    UserId = HbmContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Comments.SingleOrDefaultAsync(comment =>
                    comment.Id == commentId && comment.Text == text));
        }
    }
}