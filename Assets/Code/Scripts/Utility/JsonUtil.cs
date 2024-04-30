using System.Collections.Generic;
using Newtonsoft.Json;

namespace Utility
{
    public static class JsonUtil
    {
        public static T CreateFromJSON<T>(string jsonString)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<T>(jsonString, jsonSerializerSettings);
        }
        
        public static List<T> CreateArrayFromJSONList<T>(string jsonString)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<List<T>>(jsonString, jsonSerializerSettings);
        }
    }
}

