using System;
using Newtonsoft.Json;

[Serializable]
public class PlaceholderModel : BaseModel
{
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("key")] public string Key { get; set; }
    [JsonProperty("asset")] public ShowroomAssetModel ShowroomAssetModel { get; set; }
    [JsonProperty("type")] public PropTypeModel PropTypeModel { get; set; }
    [JsonProperty("showroom")] public ShowroomModel ShowroomModel { get; set; }
}