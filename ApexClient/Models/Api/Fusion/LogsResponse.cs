using Newtonsoft.Json;

namespace Library.Models.Api.Fusion;

public class LogsResponse
{
    [JsonProperty(PropertyName = "ilog")]
    public Logs<InputItem> InputLog { get; set; }

    [JsonProperty(PropertyName = "olog")]
    public Logs<OutputItem> OutputLog { get; set; }

    [JsonProperty(PropertyName = "mlog")]
    public Logs<TestItem> TestLog { get; set; }

    public class Logs<T>
    {
        [JsonProperty(PropertyName = "limited")]
        public bool Limited { get; set; }

        [JsonProperty(PropertyName = "items")]
        public T[] Items { get; set; }
    }
    
    
    public class InputItem
    {
        [JsonProperty(PropertyName = "date")] 
        public DateTimeOffset Date { get; set; }
        [JsonProperty(PropertyName = "inputs")]
        public InputSubItem[] Inputs { get; set; }
    }
    
    public class InputSubItem
    {
        [JsonProperty(PropertyName = "did")] 
        public string InputType { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
    }

    public class OutputItem
    {
        [JsonProperty(PropertyName = "date")] public DateTimeOffset Date { get; set; }
        [JsonProperty(PropertyName = "did")] public string OutputType { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "status")] public string Status { get; set; } = string.Empty;
    }

    public class TestItem
    {
        [JsonProperty(PropertyName = "date")]
        public DateTimeOffset Date { get; set; }
        
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set;}
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set;} = string.Empty;
        
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set;}
        
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set;} = string.Empty;
        
        public TestResult.TestType GetTestType()
        {
            switch (Type)
            {
                case 1:
                    return TestResult.TestType.Alk;
                case 2:
                    return TestResult.TestType.Calcium;
                case 4:
                    return TestResult.TestType.Magnesium;
                default:
                    return TestResult.TestType.Unknown;
            }
        }
    }
}