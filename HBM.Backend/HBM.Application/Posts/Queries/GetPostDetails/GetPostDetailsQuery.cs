using MediatR;

namespace HBM.Application.Posts.Queries.GetPostDetails
{
    public class GetPostDetailsQuery : IRequest<PostDetailsVm>
    {
        public Guid Id { get; set; }
    }
}