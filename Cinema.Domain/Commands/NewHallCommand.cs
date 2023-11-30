using Cinema.SharedModels;
using MediatR;

namespace Cinema.Domain.Commands
{
    public class NewHallCommand : IRequest<Hall>
    {
        public string Name { get; set; }

        public int SeatsPerRow { get; set; }

        public int Rows { get; set; }
    }
}
