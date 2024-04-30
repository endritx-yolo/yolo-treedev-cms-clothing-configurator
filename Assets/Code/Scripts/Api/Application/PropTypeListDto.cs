using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class PropTypeListDto
{
    [JsonProperty("data")]
    public List<PropTypeModel> PropTypeModelList { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("per_page")]
    public int PerPage { get; set; }
}
