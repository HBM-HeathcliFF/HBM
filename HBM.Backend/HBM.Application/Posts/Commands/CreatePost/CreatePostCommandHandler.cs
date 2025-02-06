using HBM.Application.Extensions;
using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;

namespace HBM.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IHbmDbContext _dbContext;

        public CreatePostCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = new Post()
            {
                UserId = request.UserId,
                Title = request.Title,
                Text = request.Text,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now.ToPostFormat(),
                EditDate = null
            };

            await _dbContext.Posts.AddAsync(post, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}