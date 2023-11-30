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
    public class MoviesQueryHandler : IRequestHandler<MoviesQuery, ListResult<Movie>>
    {
        private readonly CinemaContext cinemaContext;

        public MoviesQueryHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<ListResult<Movie>> Handle(MoviesQuery query, CancellationToken cancellationToken)
        {
            var movies = await cinemaContext.Movies
                .Select(x => new Movie
                {
                    Id = x.Id,
                    Description = x.Description,
                    Length = x.Length,
                    Title = x.Title,
                    PictureUrl = x.PictureUrl,
                })
                .ToListAsync(cancellationToken);
            return new ListResult<Movie> { List = movies };
        }
    }
}
