using HBM.Application.Common.Exceptions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;

namespace HBM.Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandHandler : IRequestHandler<DeleteReactionCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public DeleteReactionCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteReactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Reactions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null ||
                entity.UserId != request.UserId ||
                entity.PostId != request.PostId)
            {
                throw new NotFoundException(nameof(Reaction), request.Id);
            }

            _dbContext.Reactions.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}