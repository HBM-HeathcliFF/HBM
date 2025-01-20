using MediatR;

namespace HBM.Application.Reactions.Queries.GetReactionList
{
    public class GetReactionListQuery : IRequest<ReactionListVm>
    {
        public Guid PostId { get; set; }
    }
}