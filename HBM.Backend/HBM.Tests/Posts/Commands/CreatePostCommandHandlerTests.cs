using HBM.Application.Posts.Commands.CreatePost;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Posts.Commands
{
    public class CreatePostCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreatePostCommandHandler_Success()
        {
            // Arrange
            var handler = new CreatePostCommandHandler(Context);
            var postName = "post name";
            var postText = "post text";

            // Act
            var postId = await handler.Handle(
                new CreatePostCommand
                {
                    Title = postName,
                    Text = postText,
                    UserId = HbmContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Posts.SingleOrDefaultAsync(post =>
                    post.Id == postId && post.Title == postName &&
                    post.Text == postText));
        }
    }
}