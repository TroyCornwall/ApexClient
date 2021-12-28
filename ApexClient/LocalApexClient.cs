using System.Runtime.CompilerServices;
using Flurl;
using Flurl.Http;
using Library.Models.Api;
using Library.Models.Api.Fusion;
using Microsoft.Extensions.Options;
using LoginRequest = Library.Models.Api.Local.LoginRequest;

namespace Library;

public class LocalApexClient : IApexClient
{
    private readonly ApexConfig _config;
    private string _cookieValue = String.Empty;

    public LocalApexClient(IOptions<ApexConfig> config)
    {
        _config = config.Value;
    }

    private string GetUrl(string endpoint)
    {
        return $"http://{_config.IpAddress}"
            .AppendPathSegment("rest")
            .AppendPathSegment(endpoint);
    }

    public async Task LoginAsync()
    {
        var loginRequest = new LoginRequest(_config.Username, _config.Password);

        IFlurlResponse response;
        try
        {
           response = await GetUrl("login")
                .SetQueryParam("_", DateTime.Now.Ticks)
                .PostJsonAsync(loginRequest);
        }
        catch (FlurlHttpException)
        {
            throw new Exception($"Failed to login as {_config.Username}");
        }

        var resp = await response.GetJsonAsync<LoginResponse>();
        _cookieValue = resp.LoginCookie ?? String.Empty;
    }
    
    public async Task<StatusResponse> GetStatus()
    {
        var loginResponse = 
            await GetUrl("status")
                .SetQueryParam("_", DateTime.Now.Ticks)
                .WithCookie("connect.sid", _cookieValue)
                .GetJsonAsync<StatusResponse>()
            ;
        return loginResponse;
    }

    public Task<TestResult[]> GetTestResults()
    {
        //Local Apex service doesn't have test results..
        throw new NotImplementedException();
    }

    public Task<LogsResponse> GetLogs()
    {
        throw new NotImplementedException();
    }
}