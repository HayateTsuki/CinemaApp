using System;
using Cinema.SharedModels;
using MediatR;

namespace Cinema.Domain.Commands
{
    public class NewScreeningCommand : IRequest<Screening>
    {
        public int MovieId { get; set; }

        public DateTime ScreeningTime { get; set; }

        public int HallId { get; set; }

        public decimal Price { get; set; }
    }
}
