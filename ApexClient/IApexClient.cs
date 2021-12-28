using Library.Models.Api;
using Library.Models.Api.Fusion;

namespace Library;

public interface IApexClient
{
    public Task LoginAsync();
    public Task<StatusResponse> GetStatus();
    public Task<TestResult[]> GetTestResults();
}