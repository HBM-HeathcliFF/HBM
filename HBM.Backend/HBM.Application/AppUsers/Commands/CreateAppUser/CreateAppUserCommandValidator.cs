using FluentValidation;

namespace HBM.Application.AppUsers.Commands.CreateAppUser
{
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(createAppUserCommand => createAppUserCommand.Id).NotEqual(Guid.Empty);
            RuleFor(createAppUserCommand => createAppUserCommand.UserName).NotEmpty();
        }
    }
}