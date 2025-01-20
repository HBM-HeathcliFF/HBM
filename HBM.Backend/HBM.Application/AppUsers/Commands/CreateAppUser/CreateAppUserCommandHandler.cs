using HBM.Application.Interfaces;
using HBM.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HBM.Application.AppUsers.Commands.CreateAppUser
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, Unit>
    {
        private readonly IHbmDbContext _dbContext;

        public CreateAppUserCommandHandler(IHbmDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

            if (user == null)
            {
                var newUser = new AppUser()
                {
                    Id = request.Id,
                    UserName = request.UserName,
                    Role = request.Role
                };

                await _dbContext.Users.AddAsync(newUser, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}