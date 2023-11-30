using System.Threading.Tasks;
using Cinema.Domain.Data.Entities;
using Cinema.SharedModels;

namespace Cinema.Domain.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(CinemaUser user);
    }
}
