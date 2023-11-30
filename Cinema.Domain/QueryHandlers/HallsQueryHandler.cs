using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Queries;
using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.QueryHandlers
{
    public class HallsQueryHandler : IRequestHandler<HallsQuery, ListResult<Hall>>
    {
        private readonly CinemaContext cinemaContext;

        public HallsQueryHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<ListResult<Hall>> Handle(HallsQuery request, CancellationToken cancellationToken)
        {
            var halls = await cinemaContext.Halls
                .Select(x => new Hall { Id = x.Id, Name = x.Name, Rows = x.Rows, SeatsPerRow = x.SeatsPerRow })
                .ToListAsync(cancellationToken);
            ListResult<Hall> result = new()
            {
                List = halls
            };
            return result;
        }
    }
}
