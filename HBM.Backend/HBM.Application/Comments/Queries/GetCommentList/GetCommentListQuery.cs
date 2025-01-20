using MediatR;

namespace HBM.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQuery : IRequest<CommentListVm>
    {
        public Guid PostId { get; set; }
    }
}