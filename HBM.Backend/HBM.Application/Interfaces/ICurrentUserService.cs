using System.Security.Claims;

namespace HBM.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string UserName { get; }
        string Role { get; }
    }
}
