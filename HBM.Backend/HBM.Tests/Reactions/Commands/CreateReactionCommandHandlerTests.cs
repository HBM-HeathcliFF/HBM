using HBM.Application.Reactions.Commands.CreateReaction;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Reactions.Commands
{
    public class CreateReactionCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateReactionCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateReactionCommandHandler(Context);

            // Act
            var reactionId = await handler.Handle(
                new CreateReactionCommand
                {
                    PostId = HbmContextFactory.PostIdForDelete,
                    UserId = HbmContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Reactions.SingleOrDefaultAsync(reaction =>
                    reaction.Id == reactionId));
        }
    }
}