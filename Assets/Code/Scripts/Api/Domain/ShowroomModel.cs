using System;
using Newtonsoft.Json;

[Serializable]
public class ShowroomModel : AuditModel
{
    [JsonProperty("slug")]
    public string Slug { get; set; }
    [JsonProperty("model")]
    public string Model { get; set; }
    [JsonProperty("image")]
    public string Image { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("version")]
    public Int64 Version { get; set; }
    [JsonProperty("meta")]
    public string Meta { get; set; }
    [JsonProperty("generic_id")]
    public Int64? GenericId { get; set; }
    [JsonProperty("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
