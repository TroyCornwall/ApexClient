using Newtonsoft.Json;

namespace Library.Models.Api;

public class StatusResponse
{
    [JsonProperty(PropertyName = "system")]
    public System System { get; set; }
    
    [JsonProperty(PropertyName = "network")]
    public System Network { get; set; }
        
    public IEnumerable<Output> Outputs { get; set; }
    public IEnumerable<Input> Inputs { get; set; }
}

public class System
{
    [JsonProperty(PropertyName = "hostname")]
    public string Hostname { get; set; }
}

public class Input
{
    [JsonProperty(PropertyName = "did")]
    public string Did { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "value")]
    public object? Value { get; set; }
    
    public string GetWaterLevel()
    {
        return Convert.ToBoolean(Value) ? "underwater" : "above water";
    }
}

public class Output
{
    [JsonProperty(PropertyName = "did")]
    public string Did { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "gid")]
    public string Gid { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "ID")]
    public int Id { get; set; }
    
    [JsonProperty(PropertyName = "status")]
    public string[] Status { get; set; } = Array.Empty<string>();
    
    public (string, string) GetStatus()
    {
        switch (Status[0])
        {
            case "AOF":
                return ("Auto", "OFF");
            case "AON":
                return ("Auto", "ON");
            case "ON":
                return ("Manual", "ON");
            case "OFF":
                return ("Manual", "OFF");
            default:
                throw new Exception("Could not parse status");
        }
    }
}