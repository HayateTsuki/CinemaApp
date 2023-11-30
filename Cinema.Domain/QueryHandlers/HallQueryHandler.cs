using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Exceptions;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Queries;
using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.QueryHandlers
{
    public class HallQueryHandler : IRequestHandler<HallQuery, SingleResult<Hall>>
    {
        private readonly CinemaContext cinemaContext;

        public HallQueryHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<SingleResult<Hall>> Handle(HallQuery query, CancellationToken cancellationToken)
        {
            var halls = await cinemaContext.Halls
                .Select(x => new Hall { Id = x.Id, Name = x.Name, Rows = x.Rows, SeatsPerRow = x.SeatsPerRow })
                .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (halls == null)
            {
                throw new EntityNotFoundException($"Nie znaleziono sali o ID: {query.Id}.", query.Id);
            }

            return new SingleResult<Hall> { Data = halls };
        }
    }
}
