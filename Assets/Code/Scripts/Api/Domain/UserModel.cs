using System;
using Newtonsoft.Json;

[Serializable]
public class UserModel : BaseModel
{
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("role")]
    public RoleModel Role { get; set; }
}
