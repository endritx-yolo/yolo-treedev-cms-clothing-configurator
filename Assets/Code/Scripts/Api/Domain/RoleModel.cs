using System;
using Newtonsoft.Json;

[Serializable]
public class RoleModel : BaseModel
{
    [JsonProperty("scopes")]
    public string Scopes { get; set; }
}
