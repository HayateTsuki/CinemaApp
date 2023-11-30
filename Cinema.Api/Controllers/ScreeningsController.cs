using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Controllers;
using Cinema.Domain.Commands;
using Cinema.Domain.Queries;
using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    public class ScreeningsController : CinemaController
    {
        private readonly IMediator mediator;

        public ScreeningsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SingleResult<Screening>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new ScreeningQuery { Id = id });
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListResult<Screening>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int? movieId)
        {
            var result = await mediator.Send(new ScreeningsQuery() { MovieId = movieId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewScreeningCommand screening, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(screening, cancellationToken);
            return CreatedObject(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateScreeningCommand screening, CancellationToken cancellationToken)
        {
            await mediator.Send(screening, cancellationToken);
            return Ok();
        }
    }
}
