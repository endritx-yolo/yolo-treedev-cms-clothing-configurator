using System;
using Newtonsoft.Json;

[Serializable]
public class PropTypeModel : AuditModel
{
    [JsonProperty("description")]
    public string Description { get; set; }
}
