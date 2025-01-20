using FluentValidation;

namespace HBM.Application.Reactions.Commands.UpdateReaction
{
    public class UpdateReactionCommandValidator : AbstractValidator<UpdateReactionCommand>
    {
        public UpdateReactionCommandValidator()
        {
            RuleFor(updateReactionCommand => updateReactionCommand.PostId).NotEqual(Guid.Empty);
            RuleFor(updateReactionCommand => updateReactionCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateReactionCommand => updateReactionCommand.Id).NotEqual(Guid.Empty);
        }
    }
}