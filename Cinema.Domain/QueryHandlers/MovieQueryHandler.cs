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
    public class MovieQueryHandler : IRequestHandler<MovieQuery, SingleResult<Movie>>
    {
        private readonly CinemaContext cinemaContext;

        public MovieQueryHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<SingleResult<Movie>> Handle(MovieQuery query, CancellationToken cancellationToken)
        {
            var movie = await cinemaContext.Movies
                .Select(x => new Movie
                {
                    Id = x.Id,
                    Title = x.Title,
                    Length = x.Length,
                    Description = x.Description,
                    PictureUrl = x.PictureUrl,
                })
                .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (movie == null)
            {
                throw new EntityNotFoundException($"Nie znaleziono filmu o ID: {query.Id}.", query.Id);
            }

            return new SingleResult<Movie> { Data = movie };
        }
    }
}
