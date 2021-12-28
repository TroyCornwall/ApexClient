using Flurl;
using Flurl.Http;
using HtmlAgilityPack;
using Library.Models.Api;
using Library.Models.Api.Fusion;
using Microsoft.Extensions.Options;

namespace Library;

public class FusionApexClient : IApexClient
{
    private readonly ApexConfig _config;
    private const string BaseUrl = "https://apexfusion.com";
    private readonly FlurlClient _client;
    private readonly CookieJar _cookies;

    public FusionApexClient(IOptions<ApexConfig> config)
    {
        _config = config.Value;
        _client = new FlurlClient(BaseUrl);
        _cookies = new CookieJar();
    }

    private IFlurlRequest GetUrl(string endpoint)
    {
        return  //BaseUrl //https://apexfusion.com
            _client
                .Request()
                .WithCookies(_cookies)
                .AppendPathSegments("api", "apex")
                .AppendPathSegment(_config.ApexClientId)
                .AppendPathSegment(endpoint)
                .SetQueryParam("_", DateTime.Now.Ticks);
    }   

    public async Task LoginAsync()
    {
        var loginRequest = new LoginRequest(_config.Username, _config.Password);

        try
        {
            var html = await BaseUrl
                .WithCookies(_cookies)
                .AppendPathSegment("login")
                .GetStringAsync();
            
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var csrfToken = doc.DocumentNode
                .Descendants("meta")
                .Where(x=> x.Attributes.Any(x => x.Value == "csrf-token"))
                .Select(x => x.Attributes.Single(x=>x.Name == "content").Value)
                .SingleOrDefault();

            if (csrfToken == null)
                throw new Exception("Failed to get csrf-token");

            await BaseUrl
                .WithCookies(_cookies)
               .AppendPathSegment("login")
               .WithHeader("csrf-token", csrfToken)
               .SetQueryParam("_", DateTime.Now.Ticks)
               .PostJsonAsync(loginRequest);
        }
        catch (FlurlHttpException)
        {
            Console.WriteLine($"Failed to login as {_config.Username}");
            throw;
        }
    }
    
    public async Task<StatusResponse> GetStatus()
    {
        var statusResponse = 
            await GetUrl("status")
                .GetJsonAsync<StatusResponse>();
        return statusResponse;
    }

    public async Task<TestResult[]> GetTestResults()
    {
        var statusResponse = 
            await GetUrl("mlog")
                .SetQueryParam("days","365")
                .GetJsonAsync<TestResult[]>();
        return statusResponse;
        
    }
}