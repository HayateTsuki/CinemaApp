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
    public class ScreeningQueryHandler : IRequestHandler<ScreeningQuery, SingleResult<Screening>>
    {
        private readonly CinemaContext context;

        public ScreeningQueryHandler(CinemaContext context)
        {
            this.context = context;
        }

        public async Task<SingleResult<Screening>> Handle(ScreeningQuery request, CancellationToken cancellationToken)
        {
            var result = await context.Screenings
                .Include(x => x.Hall)
                .Include(x => x.Movie)
                .Select(x => new Screening
                {
                    Id = x.Id,
                    Hall = new Hall { Id = x.HallId, Name = x.Hall.Name, Rows = x.Hall.Rows, SeatsPerRow = x.Hall.SeatsPerRow },
                    Movie = new Movie { Id = x.MovieId, Title = x.Movie.Title, Description = x.Movie.Description, Length = x.Movie.Length, PictureUrl = x.Movie.PictureUrl },
                    Price = x.Price,
                    Date = x.Date
                })
                .SingleOrDefaultAsync(x => x.Id == request.Id,  cancellationToken);
            if (result is null)
            {
                throw new EntityNotFoundException($"Nie znaleziono seansu o ID: {request.Id}.", request.Id);
            }

            return new SingleResult<Screening> { Data = result };
        }
    }
}
