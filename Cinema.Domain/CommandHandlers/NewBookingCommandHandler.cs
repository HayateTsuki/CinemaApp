using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Cinema.Domain.Commands;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Data.Entities;
using Cinema.Domain.Models;
using Cinema.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.CommandHandlers
{
    public class NewBookingCommandHandler : IRequestHandler<NewBookingCommand, Unit>
    {
        private readonly CinemaContext cinemaContext;
        private readonly IUserContext userContext;

        public NewBookingCommandHandler(CinemaContext cinemaContext, IUserContext userContext)
        {
            this.cinemaContext = cinemaContext;
            this.userContext = userContext;
        }

        public async Task<Unit> Handle(NewBookingCommand request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await cinemaContext.Bookings.SingleOrDefaultAsync(x => x.ScreeningId == request.Screening.Id && x.Row == request.Row && x.Seat == request.Seat);

            if (result == null)
            {
                await cinemaContext.Bookings.AddAsync(new BookingEntity
                {
                    UserId = userContext.Id,
                    ScreeningId = request.Screening.Id,
                    Row = request.Row,
                    Seat = request.Seat,
                    IsConfirmed = request.IsConfirmed,
                    Date = request.Date,
                });

                await cinemaContext.SaveChangesAsync();
                scope.Complete();
            }
            else
            {
                scope.Complete();
                throw new Exception("Podane miejsce jest niedostępne");
            }

            return Unit.Value;
        }
    }
}
