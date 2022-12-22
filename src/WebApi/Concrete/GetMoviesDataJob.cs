
using Application.Features.Movies.Commands.CreatePageData;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
namespace WebApi.Concrete;
public class GetMoviesDataJob : IHostedService
{
    private readonly IServiceProvider _container;
    public GetMoviesDataJob(IServiceProvider container)
    {
        _container = container;
    }
    public async Task<int> GetData()
    {

        string[] pages = { "1", "2", "3", "4", "5", "6", "7" };
        foreach (var item in pages)
        {
            var query = new Dictionary<string, string>()
            {
                ["api_key"] = "dd2dfb55d24f7fa100a239cbbd787c3e",
                ["language"] = "en-US",
                ["page"] = item
            };

            var uri = QueryHelpers.AddQueryString("https://api.themoviedb.org/3/movie/2/lists", query);
            PageData root = new PageData();
            using (HttpClient httpclient = new HttpClient())
            {
                var result = await httpclient.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Response status code: {result.StatusCode}");
                    string data = await result.Content.ReadAsStringAsync();
                    root = JsonConvert.DeserializeObject<PageData>(data) ?? new PageData();
                }
            }
            using (var scope = _container.CreateScope())
            {
                var mediator1 = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator1.Send(new CreatePageDataCommand { PageData = root });
            }
        }

        return pages.Length;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () => await GetData());

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

