using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;

namespace HBM.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly IHbmDbContext _dbContext;

        public CreateCommentCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment()
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Text = request.Text,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}