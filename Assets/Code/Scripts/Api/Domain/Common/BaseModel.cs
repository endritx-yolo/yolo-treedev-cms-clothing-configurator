using Newtonsoft.Json;

public class BaseModel
{
    [JsonProperty("id")] public uint Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
}