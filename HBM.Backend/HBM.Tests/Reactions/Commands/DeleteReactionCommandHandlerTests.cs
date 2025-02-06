using HBM.Application.Comments.Commands.CreateComment;
using HBM.Application.Comments.Commands.DeleteComment;
using HBM.Application.Common.Exceptions;
using HBM.Application.Reactions.Commands.CreateReaction;
using HBM.Application.Reactions.Commands.DeleteReaction;
using HBM.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace HBM.Tests.Reactions.Commands
{
    public class DeleteReactionCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteReactionCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteReactionCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteReactionCommand
            {
                Id = HbmContextFactory.ReactionIdForDelete,
                UserId = HbmContextFactory.UserBId
            },
            CancellationToken.None);

            // Assert
            Assert.Null(await Context.Reactions.SingleOrDefaultAsync(reaction =>
                reaction.Id == HbmContextFactory.ReactionIdForDelete));
        }

        [Fact]
        public async Task DeleteReactionCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteReactionCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteReactionCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = HbmContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteReactionCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteReactionCommandHandler(Context);
            var createHandler = new CreateReactionCommandHandler(Context);
            var reactionId = await createHandler.Handle(
                new CreateReactionCommand
                {
                    UserId = HbmContextFactory.UserBId,
                    PostId = HbmContextFactory.PostIdForDelete
                },
                CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteReactionCommand
                    {
                        Id = reactionId,
                        UserId = HbmContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }
    }
}