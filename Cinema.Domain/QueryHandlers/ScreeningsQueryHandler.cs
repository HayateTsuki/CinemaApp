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
    public class ScreeningsQueryHandler : IRequestHandler<ScreeningsQuery, ListResult<Screening>>
    {
        private readonly CinemaContext context;

        public ScreeningsQueryHandler(CinemaContext context)
        {
            this.context = context;
        }

        public async Task<ListResult<Screening>> Handle(ScreeningsQuery request, CancellationToken cancellationToken)
        {
           var result = await context.Screenings
                .Include(x => x.Hall)
                .Include(x => x.Movie).Where(x => (request.MovieId.HasValue && request.MovieId == x.MovieId) || !request.MovieId.HasValue)
                .Select(x => new Screening
                {
                    Id = x.Id,
                    Hall = new Hall { Id = x.HallId, Name = x.Hall.Name, Rows = x.Hall.Rows, SeatsPerRow = x.Hall.SeatsPerRow },
                    Movie = new Movie { Id = x.MovieId, Title = x.Movie.Title, Description = x.Movie.Description, Length = x.Movie.Length, PictureUrl = x.Movie.PictureUrl },
                    Price = x.Price,
                    Date = x.Date
                })
                .ToListAsync(cancellationToken);

           return new ListResult<Screening> { List = result };
        }
    }
}
