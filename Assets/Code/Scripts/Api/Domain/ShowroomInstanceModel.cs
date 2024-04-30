using System;
using Newtonsoft.Json;

[Serializable]
public class ShowroomInstanceModel : BaseModel
{
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("user")] public UserModel UserModel { get; set; }
    [JsonProperty("key")] public string Key { get; set; }
    [JsonProperty("showroom")] public ShowroomModel ShowroomModel { get; set; }
}