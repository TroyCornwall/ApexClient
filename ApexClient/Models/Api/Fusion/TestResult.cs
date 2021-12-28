using Newtonsoft.Json;

namespace Library.Models.Api.Fusion;

public class TestResult
{
    [JsonProperty(PropertyName = "_id")]
    public string Id { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "date")]
    public DateTimeOffset Date { get; set; }
    
    [JsonProperty(PropertyName = "type")]
    public int Type { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "Value")]
    public double Value { get; set; }
        
    [JsonProperty(PropertyName = "text")]
    public string Text { get; set; } = String.Empty;
    
    [JsonProperty(PropertyName = "__v")]
    public int V { get; set; }

    public TestType GetTestType()
    {
        switch (Type)
        {
            case 1:
                return TestType.Alk;
            case 2:
                return TestType.Calcium;
            case 4:
                return TestType.Magnesium;
            default:
                return TestType.Unknown;
        }
    }
    
    public enum TestType
    {
        Unknown,
        Alk,
        Calcium,
        Magnesium
    }
}
