using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Exceptions;
using Cinema.Domain.Commands;
using Cinema.Domain.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.CommandHandlers
{
    public class UpdateHallCommandHandler : IRequestHandler<UpdateHallCommand, Unit>
    {
        private readonly CinemaContext cinemaContext;

        public UpdateHallCommandHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<Unit> Handle(UpdateHallCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            if (request.SeatsPerRow <= 0)
            {
                throw new ArgumentException(nameof(request.SeatsPerRow));
            }

            if (request.Rows <= 0)
            {
                throw new ArgumentException(nameof(request.Rows));
            }

            var existingHall = cinemaContext.Halls.SingleOrDefault(h => h.Id == request.Id);
            if (existingHall == null)
            {
                throw new EntityNotFoundException($"Sala o id {request.Id} nie została znaleziona", request.Id);
            }

            var existingNameHalls = await cinemaContext.Halls.AnyAsync(x => x.Id != request.Id && request.Name == x.Name);
            if (existingNameHalls)
            {
                throw new EnitityAlreadyExists($"Sala o nazwie {request.Name} już istnieje");
            }

            existingHall.Name = request.Name;
            existingHall.SeatsPerRow = request.SeatsPerRow;
            existingHall.Rows = request.Rows;
            cinemaContext.Update(existingHall);
            await cinemaContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
