using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Exceptions;
using Cinema.Domain.Data.Entities;
using Cinema.Domain.Queries;
using Cinema.Domain.Results;
using Cinema.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Domain.QueryHandlers
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserResult>
    {
        private readonly UserManager<CinemaUser> userManager;
        private readonly IIdentityService identityService;

        public LoginUserQueryHandler(UserManager<CinemaUser> userManager, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
        }

        public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BusinessOperationException("Nieprawidłowy email lub hasło.");
            }

            if (await userManager.IsLockedOutAsync(user))
            {
                throw new BusinessOperationException("Konto użytkownika jest zablokowane.");
            }

            var userHasValidPassword = await userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasValidPassword)
            {
                await userManager.AccessFailedAsync(user);
                throw new BusinessOperationException("Nieprawidłowy email lub hasło.");
            }

            await userManager.ResetAccessFailedCountAsync(user);
            await userManager.SetLockoutEndDateAsync(user, null);

            var identityResult = await identityService.GenerateAuthenticationResultForUserAsync(user);
            var result = new LoginUserResult
            {
                Id = user.Id,
                Email = user.Email,
                Token = identityResult.Token,
                UserName = user.UserName
            };

            return result;
        }
    }
}
