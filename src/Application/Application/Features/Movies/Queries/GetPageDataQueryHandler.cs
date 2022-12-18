using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
namespace Application.Features.Movies.Queries
{
    public class GetPageDataQueryHandler : IRequestHandler<GetPageDataQuery, List<Movie>>
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IMovieRepository _movieRepository;
        public GetPageDataQueryHandler(IPageDataRepository pageDataRepository, IMovieRepository movieRepository)
        {
            _pageDataRepository = pageDataRepository ?? throw new ArgumentNullException(nameof(pageDataRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));

        }

        public async Task<List<Movie>> Handle(GetPageDataQuery request, CancellationToken cancellationToken)
        {
            int pageDataId = _pageDataRepository.GetAllAsync().GetAwaiter().GetResult().FirstOrDefault(x => x.Page == request.PageId).Id;
            List<Movie> result = new List<Movie>();
            if (pageDataId > 0)
            {
                result = _movieRepository.GetAllAsync().GetAwaiter().GetResult().Where(x => x.PageId == pageDataId).ToList();
            }
            return result;
        }
    }
}
