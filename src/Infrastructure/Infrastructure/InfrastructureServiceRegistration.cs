using Application.Contracts.Persistence;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MovieConnectionString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
           
            services.AddScoped<IPageDataRepository, PageDataRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            return services;
        }
    }
}
