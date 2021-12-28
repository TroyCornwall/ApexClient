using Newtonsoft.Json;

namespace Library.Models.Api;

public class LoginResponse
{
    [JsonProperty(PropertyName = "connect.sid")]
    public string? LoginCookie { get; set; }
}