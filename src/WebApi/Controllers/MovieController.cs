using Domain.Entities;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Movie")]
    public async Task<PageData> Get(string pagenumber)
    {


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
        return root;
    }
}
