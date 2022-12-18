
using Application.Features.Movies.Commands.CreatePageData;
using Domain.Entities;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebApi.Interface;
namespace WebApi.Concrete;
public class GetMoviesDataJob : IGetMoviesDataJob
{
    private readonly IMediator _mediator;
    public GetMoviesDataJob(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Obsolete]
    public async Task GetMovieData()
    {
        RecurringJob.AddOrUpdate(() => Console.Write("Easy!"), Cron.MinuteInterval(1), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));//dakikada 1
        await GetData();
        await Task.CompletedTask;
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
}

