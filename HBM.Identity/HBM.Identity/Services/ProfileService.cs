using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using HBM.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HBM.Identity.Services
{
    public class ProfileService : IProfileService
    {
        IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        UserManager<AppUser> _userManager;

        public ProfileService(
            IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager) =>
            (_userClaimsPrincipalFactory, _userManager) = (userClaimsPrincipalFactory, userManager);

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            AppUser user = await _userManager.FindByIdAsync(sub);

            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(u => context.RequestedClaimTypes.Contains(u.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.Name, user.UserName));

            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var rolename in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, rolename));
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            AppUser user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}