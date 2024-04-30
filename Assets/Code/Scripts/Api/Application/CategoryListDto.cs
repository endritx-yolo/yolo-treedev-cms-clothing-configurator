using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class CategoryListDto
{
    [JsonProperty("data")]
    public List<CategoryModel> CategoryModelList { get; set; }

    [JsonProperty("page_count")]
    public float PageCount { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("per_page")]
    public int PerPage { get; set; }
}
