using Domain.Entities;
using MediatR;
namespace Application.Features.Movies.Commands.CreatePageData;
public class CreatePageDataCommand : IRequest<PageData>
{
    public PageData PageData { get; set; }  
}
