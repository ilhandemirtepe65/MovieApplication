


using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;


namespace Infrastructure.Repositories
{
    public class PageDataRepository : RepositoryBase<PageData>, IPageDataRepository
    {
        public PageDataRepository(MovieContext dbContext) : base(dbContext)
        {
        }

    }
}
