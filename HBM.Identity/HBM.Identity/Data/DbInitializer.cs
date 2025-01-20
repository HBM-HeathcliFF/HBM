﻿using Duende.IdentityModel;
using HBM.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HBM.Identity.Data
{
    public class DbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) =>
            (_userManager, _roleManager) = (userManager, roleManager);

        public void Initialize(AuthDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                _roleManager.CreateAsync(new IdentityRole("Owner")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();

                AppUser owner = new()
                {
                    UserName = "Heathcliff"
                };

                _userManager.CreateAsync(owner, "dfU_c21k5sr").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(owner, "Owner").GetAwaiter().GetResult();

                var claims = _userManager.AddClaimsAsync(owner, new Claim[]
                {
                new Claim(JwtClaimTypes.Name, owner.UserName),
                new Claim(JwtClaimTypes.Role, "Owner")
                }).Result;
            }
        }
    }
}