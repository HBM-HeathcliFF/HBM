using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;

namespace HBM.Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommandHandler : IRequestHandler<CreateReactionCommand, Guid>
    {
        private readonly IHbmDbContext _dbContext;

        public CreateReactionCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = new Reaction()
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Id = Guid.NewGuid()
            };

            await _dbContext.Reactions.AddAsync(reaction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return reaction.Id;
        }
    }
}