using HBM.Application.Common.Exceptions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;

namespace HBM.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public DeletePostCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Posts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            _dbContext.Posts.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}