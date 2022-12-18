
using Domain.Entities;
using MediatR;

namespace Application.Features.Movies.Queries
{
    public class GetPageDataQuery : IRequest<List<Movie>>
    {
        public int PageId { get; set; }

    }
}
