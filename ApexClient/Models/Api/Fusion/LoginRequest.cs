using Newtonsoft.Json;

namespace Library.Models.Api.Fusion;

public class LoginRequest
{
    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    [JsonProperty(PropertyName = "username")]
    public string Username { get; set; }
    
    [JsonProperty(PropertyName = "password")]
    public string Password { get; set; }
    
    [JsonProperty(PropertyName = "remember_me")]
    public bool RememberMe = false;
}