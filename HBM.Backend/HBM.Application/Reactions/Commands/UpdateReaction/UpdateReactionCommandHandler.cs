using HBM.Application.Common.Exceptions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Reactions.Commands.UpdateReaction
{
    public class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public UpdateReactionCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Reactions.FirstOrDefaultAsync(reaction =>
                reaction.Id == request.Id, cancellationToken);

            var post = await _dbContext.Posts.FirstOrDefaultAsync(post =>
                post.Id == request.PostId, cancellationToken);

            if (post == null || post.Id != request.PostId ||
                entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Reaction), request.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}