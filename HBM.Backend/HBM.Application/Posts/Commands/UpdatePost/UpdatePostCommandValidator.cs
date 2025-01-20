using FluentValidation;

namespace HBM.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(updatePostCommand => updatePostCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updatePostCommand => updatePostCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updatePostCommand => updatePostCommand.Title).NotEmpty().MaximumLength(250);
        }
    }
}