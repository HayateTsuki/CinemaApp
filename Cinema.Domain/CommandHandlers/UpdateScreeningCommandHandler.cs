using System;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Domain.Commands;
using Cinema.Domain.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.CommandHandlers
{
    public class UpdateScreeningCommandHandler : IRequestHandler<UpdateScreeningCommand, Unit>
    {
        private readonly CinemaContext cinemaContext;

        public UpdateScreeningCommandHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<Unit> Handle(UpdateScreeningCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ScreeningId <= 0)
            {
                throw new ArgumentException(nameof(request.ScreeningId));
            }

            if (request.Price < 0)
            {
                throw new ArgumentException(nameof(request.Price));
            }

            if (request.ScreeningTime < DateTime.UtcNow)
            {
                throw new ArgumentException(nameof(request.ScreeningTime));
            }

            var existingScreening = await cinemaContext.Screenings.Include(x => x.Hall).SingleOrDefaultAsync(x => x.Id == request.ScreeningId);
            if (existingScreening == null)
            {
                throw new Exception($"Publikacja danego filmu o id {request.ScreeningId} nie została znaleziona");
            }

            var busySlot = await cinemaContext.Screenings.Include(x => x.Movie).AnyAsync(x => x.Date.AddMinutes(-30) <= request.ScreeningTime.ToUniversalTime() && x.Date.AddMinutes(30 + x.Movie.Length.TotalMinutes) >= request.ScreeningTime.ToUniversalTime() && x.Id != request.ScreeningId);
            if (busySlot)
            {
                throw new Exception($"Brak wolnego miejsca na sali {existingScreening.Hall.Name} do odtworzenia filmu o podanym czasie {request.ScreeningTime}");
            }

            existingScreening.Price = request.Price;
            existingScreening.Date = request.ScreeningTime.ToUniversalTime();
            cinemaContext.Screenings.Update(existingScreening);
            await cinemaContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
