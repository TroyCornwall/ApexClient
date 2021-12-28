using Newtonsoft.Json;

namespace Library.Models.Api.Local;

internal class LoginRequest
{
    public LoginRequest(string login, string password)
    {
        Login = login;
        Password = password;
    }
    
    [JsonProperty(PropertyName = "login")]
    public string Login { get; set; }
    
    [JsonProperty(PropertyName = "password")]
    public string Password { get; set; }
    
    [JsonProperty(PropertyName = "remember_me")]
    public bool RememberMe = false;
}