using System;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Core.Exceptions;
using Cinema.Domain.Commands;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Data.Entities;
using Cinema.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.CommandHandlers
{
    public class NewHallCommandHandler : IRequestHandler<NewHallCommand, Hall>
    {
        private readonly CinemaContext cinemaContext;

        public NewHallCommandHandler(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public async Task<Hall> Handle(NewHallCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Rows <= 0)
            {
                throw new ArgumentException(nameof(request.Rows));
            }

            if (request.SeatsPerRow <= 0)
            {
                throw new ArgumentException(nameof(request.SeatsPerRow));
            }

            var existingNameHalls = await cinemaContext.Halls.AnyAsync(x => x.Name == request.Name);
            if (existingNameHalls)
            {
                throw new EnitityAlreadyExists($"Sala o nazwie {request.Name} już istnieje");
            }

            var hall = new HallEntity();
            hall.SeatsPerRow = request.SeatsPerRow;
            hall.Rows = request.Rows;
            hall.Name = request.Name;
            var newHall = await cinemaContext.AddAsync(hall);
            await cinemaContext.SaveChangesAsync();
            var newViewHall = new Hall
            {
                Id = hall.Id,
                Name = hall.Name,
                SeatsPerRow = hall.SeatsPerRow,
                Rows = hall.Rows,
            };
            return newViewHall;
        }
    }
}
