using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;

namespace Cinema.Domain.Queries
{
    public record BookingQuery(int Id) : IRequest<SingleResult<Booking>>;
}
