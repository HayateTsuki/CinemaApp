using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;

namespace Cinema.Domain.Queries
{
    public class HallsQuery : IRequest<ListResult<Hall>>
    {
    }
}
