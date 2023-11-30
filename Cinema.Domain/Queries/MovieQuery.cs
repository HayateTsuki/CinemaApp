using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;

namespace Cinema.Domain.Queries
{
    public record MovieQuery(int Id) : IRequest<SingleResult<Movie>>;
}
