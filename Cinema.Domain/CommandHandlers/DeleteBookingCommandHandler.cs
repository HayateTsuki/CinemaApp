using System;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Domain.Commands;
using Cinema.Domain.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.CommandHandlers
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly CinemaContext cinemaContext;

        public DeleteBookingCommandHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var result = await cinemaContext.Bookings.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (result == null)
            {
                throw new Exception($"Rezerwacja o ID {request.Id} nie istnieje");
            }

            cinemaContext.Bookings.Remove(result);
            await cinemaContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
