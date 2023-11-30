using System;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Controllers;
using Cinema.Domain.Commands;
using Cinema.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    public class HallsController : CinemaController
    {
        private readonly IMediator mediator;

        public HallsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new HallsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new HallQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewHallCommand newHallCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(newHallCommand, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateHallCommand updateHallCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(updateHallCommand, cancellationToken);
            return Ok();
        }
    }
}
