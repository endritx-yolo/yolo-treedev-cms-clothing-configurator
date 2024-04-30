using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class ShowroomInstancePlaceholderAssetDto
{
    [JsonProperty("prop")]
    public List<PlaceholderModel> PlaceholderModelList { get; set; }
}
