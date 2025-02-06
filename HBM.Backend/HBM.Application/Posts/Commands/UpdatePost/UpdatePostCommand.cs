using MediatR;

namespace HBM.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}