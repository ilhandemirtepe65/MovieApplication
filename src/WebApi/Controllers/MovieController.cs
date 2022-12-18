using Application.Features.Movies.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModel;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

    private readonly ILogger<MovieController> _logger;
    private readonly IMediator _mediator;
    public MovieController(ILogger<MovieController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet(Name = "Movie")]
    public async Task<List<MovieViewModel>> Get(int pagenumber)
    {
        List<Movie> movies = await _mediator.Send(new GetPageDataQuery { PageId = pagenumber });
        List<MovieViewModel> lst = movies.Select(x => new MovieViewModel
        {
            Description = x.Description,
            Favorite_count = x.Favorite_count,
            Id = x.Id,
            Iso_639_1 = x.Iso_639_1,
            Item_count = x.Item_count,
            list_type = x.list_type,
            Name = x.Name
        }).ToList();
        return lst;
    }
}
