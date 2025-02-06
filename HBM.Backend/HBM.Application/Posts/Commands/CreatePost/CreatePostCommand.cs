using MediatR;

namespace HBM.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}