using System;
using Newtonsoft.Json;

[Serializable]
public class ShowroomAssetModel : BaseModel
{
    [JsonProperty("category")] public CategoryModel Category { get; set; }
    [JsonProperty("user")] public UserModel User { get; set; }
    [JsonProperty("type")] public PropTypeModel PropType { get; set; }
    [JsonProperty("image")] public string FileName { get; set; }
    [JsonProperty("original_name")] public string ImageName{ get; set; }
    [JsonProperty("object")] public string Object { get; set; }
    [JsonProperty("status")] public Int64 Status { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    //[JsonProperty("properties")] public string Properties { get; set; }
}