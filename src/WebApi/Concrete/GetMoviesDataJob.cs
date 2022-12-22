
using Application.Features.Movies.Commands.CreatePageData;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using NCrontab;
using Newtonsoft.Json;
namespace WebApi.Concrete;
public class GetMoviesDataJob : IHostedService
{
    private readonly IMediator _mediator;
    public GetMoviesDataJob(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<PageData> GetData()
    {
        string pagenumber = "1";

        var query = new Dictionary<string, string>()
        {
            ["api_key"] = "dd2dfb55d24f7fa100a239cbbd787c3e",
            ["language"] = "en-US",
            ["page"] = pagenumber
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
        await _mediator.Send(new CreatePageDataCommand { PageData = root });
        return root;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        GetData();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    Task.Run(() =>
    //    {
    //        while (true)
    //        {
    //            GetData();
    //            Task.Delay(1000);
    //        }
    //    }));

    //    return Task.CompletedTask;




    //}
}

