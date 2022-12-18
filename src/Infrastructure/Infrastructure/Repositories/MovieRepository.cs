using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence;
namespace Infrastructure.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext dbContext) : base(dbContext)
        {
        }
    }
}
