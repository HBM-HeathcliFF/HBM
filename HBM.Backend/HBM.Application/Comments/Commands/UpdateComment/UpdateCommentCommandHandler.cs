using HBM.Application.Common.Exceptions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public UpdateCommentCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Comments.FirstOrDefaultAsync(comment =>
                comment.Id == request.Id, cancellationToken);

            var post = await _dbContext.Posts.FirstOrDefaultAsync(post =>
                post.Id == request.PostId, cancellationToken);

            if (post == null || post.Id != request.PostId ||
                entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Comment), request.Id);
            }

            entity.Text = request.Text;
            entity.EditDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}