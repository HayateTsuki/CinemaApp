using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;

namespace Cinema.Domain.Queries
{
    public class ScreeningQuery : IRequest<SingleResult<Screening>>
    {
        public int Id { get; set; }
    }
}
