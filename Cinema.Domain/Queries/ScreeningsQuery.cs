using Cinema.SharedModels;
using Cinema.SharedModels.BaseModels;
using MediatR;

namespace Cinema.Domain.Queries
{
    public class ScreeningsQuery : IRequest<ListResult<Screening>>
    {
        public int? MovieId { get; set; }
    }
}
