using HBM.Application.Interfaces;
using System.Security.Claims;

namespace HBM.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }

        public string UserName
        {
            get
            {
                var name = _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
                return string.IsNullOrEmpty(name) ? string.Empty : name;
            }
        }

        public string Role
        {
            get
            {
                var role = _httpContextAccessor.HttpContext?.User?
                    .FindFirstValue(ClaimTypes.Role);
                return string.IsNullOrEmpty(role) ? string.Empty : role;
            }
        }
    }
}