using MediatR;

namespace Cinema.Domain.Commands
{
    public class DeleteBookingCommand : IRequest
    {
        public int Id { get; set; }
    }
}
