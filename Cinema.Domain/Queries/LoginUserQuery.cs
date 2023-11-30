using Cinema.Domain.Results;
using MediatR;

namespace Cinema.Domain.Queries
{
    public class LoginUserQuery : IRequest<LoginUserResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
