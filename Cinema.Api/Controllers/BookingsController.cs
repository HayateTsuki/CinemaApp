using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Controllers;
using Cinema.Domain.Commands;
using Cinema.Domain.Models;
using Cinema.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    public class BookingsController : CinemaController
    {
        private readonly IMediator mediator;

        public BookingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new BookingsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new BookingQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewBookingCommand payload, CancellationToken cancellationToken)
        {
            await mediator.Send(payload, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteBookingCommand { Id = id }, cancellationToken);
            return Ok();
        }
    }
}
