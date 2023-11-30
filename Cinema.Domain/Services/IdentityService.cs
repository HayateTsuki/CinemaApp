using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Data.Entities;
using Cinema.Domain.Settings;
using Cinema.SharedModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Domain.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<CinemaUser> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly JwtSettings jwtSettings;
        private readonly CinemaContext context;

        public IdentityService(UserManager<CinemaUser> userManager, JwtSettings jwtSettings, CinemaContext context, RoleManager<IdentityRole<int>> roleManager)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
            this.context = context;
            this.roleManager = roleManager;
        }

        public async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(CinemaUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.GivenName, user.UserName),
                new("id", user.Id.ToString())
            };

            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await roleManager.FindByNameAsync(userRole);
                if (role == null)
                {
                    continue;
                }

                var roleClaims = await roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                    {
                        continue;
                    }

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.LastLoginDate = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Token = tokenHandler.WriteToken(token),
            };
        }
    }
}
