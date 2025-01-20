using HBM.Application.Common.Exceptions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;

namespace HBM.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public DeleteCommentCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Comments.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null ||
                entity.UserId != request.UserId ||
                entity.PostId != request.PostId)
            {
                throw new NotFoundException(nameof(Comment), request.Id);
            }

            _dbContext.Comments.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}