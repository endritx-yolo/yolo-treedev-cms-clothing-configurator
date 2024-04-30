using System;
using Newtonsoft.Json;

[Serializable]
public class InstancePlaceholderAssetDto
{
    [JsonProperty("prop")]
    public PlaceholderModel PlaceholderModel { get; set; }
    [JsonProperty("asset_assigned")]
    public ShowroomAssetModel ShowroomAssetModel { get; set; }
}
