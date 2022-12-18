using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.Features.Movies.Commands.CreatePageData;
public class CreatePageDataCommandHandler : IRequestHandler<CreatePageDataCommand, PageData>
{
    private readonly IPageDataRepository _pageDataRepository;
    private readonly ILogger<CreatePageDataCommandHandler> _logger;
    public CreatePageDataCommandHandler(IPageDataRepository pageDataRepository, ILogger<CreatePageDataCommandHandler> logger)
    {
        _pageDataRepository = pageDataRepository ?? throw new ArgumentNullException(nameof(pageDataRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<PageData> Handle(CreatePageDataCommand request, CancellationToken cancellationToken)
    {
        PageData pageData = new PageData();
        pageData.CreatedBy = request.PageData.CreatedBy;
        pageData.CreatedDate = request.PageData.CreatedDate;
        pageData.LastModifiedBy = request.PageData.LastModifiedBy;
        pageData.LastModifiedDate = request.PageData.LastModifiedDate;
        pageData.Page = request.PageData.Page;
        pageData.Total_pages = request.PageData.Total_pages;
        pageData.Total_results = request.PageData.Total_results;
        List<Movie> movies = new List<Movie>();
        foreach (var item in request.PageData.Results)
        {
            movies.Add(new Movie
            {
                CreatedBy = item.CreatedBy,
                Iso_639_1 = item.Iso_639_1,
                CreatedDate = item.CreatedDate,
                Description = item.Description,
                Favorite_count = item.Favorite_count,
                Item_count = item.Item_count,
                Name = item.Name,
                LastModifiedBy = item.LastModifiedBy,
                LastModifiedDate = item.LastModifiedDate,
                list_type = item.list_type
            });
        }
        pageData.Results = movies;  
        var isexist = _pageDataRepository.GetAllAsync().Result.FirstOrDefault(x => x.Page == request.PageData.Page);
        if (isexist == null)
        {
            await _pageDataRepository.AddAsync(pageData);
        }
        return null;
    }
}

