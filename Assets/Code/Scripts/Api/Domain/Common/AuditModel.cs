using System;
using Newtonsoft.Json;

public class AuditModel : BaseModel
{
    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; set; }
    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
