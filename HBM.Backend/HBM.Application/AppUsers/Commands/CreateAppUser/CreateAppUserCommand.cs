using MediatR;

namespace HBM.Application.AppUsers.Commands.CreateAppUser
{
    public class CreateAppUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}