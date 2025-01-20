using MediatR;

namespace HBM.Application.Reactions.Commands.UpdateReaction
{
    public class UpdateReactionCommand : IRequest<Unit>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}