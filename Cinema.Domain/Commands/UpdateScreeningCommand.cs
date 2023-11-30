using System;
using MediatR;

namespace Cinema.Domain.Commands
{
    public class UpdateScreeningCommand : IRequest
    {
        public int ScreeningId { get; set; }

        public decimal Price { get; set; }

        public DateTime ScreeningTime { get; set; }

        public int HallId { get; set; }

        public int MovieId { get; set; }
    }
}
