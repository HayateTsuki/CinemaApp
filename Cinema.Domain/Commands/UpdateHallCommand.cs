using MediatR;

namespace Cinema.Domain.Commands
{
    public class UpdateHallCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SeatsPerRow { get; set; }

        public int Rows { get; set; }
    }
}
