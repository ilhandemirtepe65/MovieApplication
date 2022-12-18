
using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence;
namespace Infrastructure.Repositories
{
    public class PageDataRepository : RepositoryBase<PageData>, IPageDataRepository
    {
        public PageDataRepository(MovieContext dbContext) : base(dbContext)
        {
        }
    }
}
