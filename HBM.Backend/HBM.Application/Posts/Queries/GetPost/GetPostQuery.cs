using MediatR;

namespace HBM.Application.Posts.Queries.GetPostDetails
{
    public class GetPostQuery : IRequest<PostVm>
    {
        public Guid Id { get; set; }
    }
}