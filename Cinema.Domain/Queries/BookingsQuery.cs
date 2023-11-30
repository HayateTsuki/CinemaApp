using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;

namespace Cinema.Domain.Queries
{
    public record BookingsQuery() : IRequest<ListResult<Booking>>;
}
