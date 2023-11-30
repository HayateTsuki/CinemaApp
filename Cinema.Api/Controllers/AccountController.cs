using System.Threading.Tasks;
using Cinema.Core.Controllers;
using Cinema.Domain.Queries;
using Cinema.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    public class AccountController : CinemaController
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(LoginUserResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginUserQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
