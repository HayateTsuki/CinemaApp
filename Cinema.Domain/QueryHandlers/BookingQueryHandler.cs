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
    public class BookingQueryHandler : IRequestHandler<BookingQuery, SingleResult<Booking>>
    {
        private readonly CinemaContext cinemaContext;

        public BookingQueryHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<SingleResult<Booking>> Handle(BookingQuery query, CancellationToken cancellationToken)
        {
            var booking = await cinemaContext.Bookings
                .Include(x => x.Screening)
                .ThenInclude(x => x.Hall)
                .Include(x => x.Screening)
                .ThenInclude(x => x.Movie)
                .Select(x => new Booking
            {
                Id = x.Id,
                Date = x.Date,
                Row = x.Row,
                Seat = x.Seat,
                IsConfirmed = x.IsConfirmed,
                Screening = new Screening
                {
                    Id = x.ScreeningId, Date = x.Screening.Date, Price = x.Screening.Price,
                    Hall = new Hall { Id = x.Screening.HallId, Name = x.Screening.Hall.Name },
                    Movie = new Movie { Id = x.Screening.MovieId, Title = x.Screening.Movie.Title, Description = x.Screening.Movie.Description, Length = x.Screening.Movie.Length, PictureUrl = x.Screening.Movie.PictureUrl }
                }
            })
                .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (booking == null)
            {
                throw new EntityNotFoundException($"Nie znaleziono rezerwacji o ID: {query.Id}.", query.Id);
            }

            return new SingleResult<Booking> { Data = booking };
        }
    }
}
