using System.Collections.Generic;
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
    public class BookingsQueryHandler : IRequestHandler<BookingsQuery, ListResult<Booking>>
    {
        private readonly CinemaContext cinemaContext;

        public BookingsQueryHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<ListResult<Booking>> Handle(BookingsQuery query, CancellationToken cancellationToken)
        {
            List<Booking> bookings = await cinemaContext.Bookings
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
                        Id = x.ScreeningId,
                        Date = x.Screening.Date,
                        Price = x.Screening.Price,
                        Hall = new Hall { Id = x.Screening.HallId, Name = x.Screening.Hall.Name },
                        Movie = new Movie { Id = x.Screening.MovieId, Title = x.Screening.Movie.Title, Description = x.Screening.Movie.Description, Length = x.Screening.Movie.Length }
                    }
                })
                .ToListAsync(cancellationToken);

            return new ListResult<Booking> { List = bookings };
        }
    }
}
