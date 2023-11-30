using System.Threading.Tasks;
using Cinema.Core.Controllers;
using Cinema.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    public class MoviesController : CinemaController
    {
        private readonly IMediator mediator;

        public MoviesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new MoviesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new MovieQuery(id));
            return Ok(result);
        }
    }
}
