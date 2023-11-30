using System;
using Cinema.SharedModels;
using MediatR;

namespace Cinema.Domain.Commands
{
    public record NewBookingCommand() : IRequest
    {
        public int UserId { get; set; }

        public Screening Screening { get; set; }

        public int Row { get; set; }

        public int Seat { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime Date { get; set; }
    }
}
