using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Cinema.Domain.Commands;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Data.Entities;
using Cinema.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.CommandHandlers
{
    public class NewScreeningCommandHandler : IRequestHandler<NewScreeningCommand, Screening>
    {
        private readonly CinemaContext cinemaContext;

        public NewScreeningCommandHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<Screening> Handle(NewScreeningCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.MovieId <= 0)
            {
                throw new ArgumentException(nameof(request.MovieId));
            }

            if (request.HallId <= 0)
            {
                throw new ArgumentException(nameof(request.HallId));
            }

            if (request.ScreeningTime < DateTime.UtcNow)
            {
                throw new ArgumentException(nameof(request.ScreeningTime));
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var databaseHall = await cinemaContext.Halls
                .Include(x => x.Screenings)
                .ThenInclude(x => x.Movie)
                .SingleOrDefaultAsync(h => h.Id == request.HallId);
            if (databaseHall == null)
            {
                scope.Complete();
                throw new Exception($"Hala o Id {request.HallId} nie została znaleziona");
            }

            var databaseMovie = await cinemaContext.Movies.SingleOrDefaultAsync(x => x.Id == request.MovieId);

            if (databaseMovie == null)
            {
                scope.Complete();
                throw new Exception($"Film o Id {request.MovieId} nie został znaleziony");
            }

            var busySlot = databaseHall.Screenings.FirstOrDefault(x => x.Date.AddMinutes(-30) <= request.ScreeningTime && x.Date.AddMinutes(30) + x.Movie.Length >= request.ScreeningTime);

            if (busySlot is not null)
            {
                scope.Complete();
                throw new Exception($"Brak wolnego miejsca na sali {databaseHall.Name} do odtworzenia filmu {databaseMovie.Title} o podanym czasie {request.ScreeningTime}");
            }

            var screening = new ScreeningEntity();
            screening.MovieId = request.MovieId;
            screening.Price = request.Price;
            screening.HallId = request.HallId;
            screening.Date = request.ScreeningTime.ToUniversalTime();
            var newScreening = await cinemaContext.AddAsync(screening, cancellationToken);
            await cinemaContext.SaveChangesAsync(cancellationToken);
            var newViewScreening = new Screening
            {
                Id = screening.Id,
                Price = screening.Price,
                Date = screening.Date,
                Movie = new Movie
                {
                    Id = screening.MovieId,
                    Description = screening.Movie.Description,
                    Length = screening.Movie.Length,
                    PictureUrl = screening.Movie.PictureUrl,
                    Title = screening.Movie.Title
                },
                Hall = new Hall
                {
                    Id = screening.HallId,
                    Name = screening.Hall.Name,
                    Rows = screening.Hall.Rows,
                    SeatsPerRow = screening.Hall.SeatsPerRow
                }
            };
            scope.Complete();
            return newViewScreening;
        }
    }
}
