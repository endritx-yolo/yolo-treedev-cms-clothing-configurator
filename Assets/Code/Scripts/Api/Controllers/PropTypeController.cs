using System;
using BestHTTP;
using Utility;

public static class PropTypeController
{
    public static void GetById(uint id, Action<PropTypeModel> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.PropTypes, id);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            PropTypeModel showroomInstanceModel = JsonUtil.CreateFromJSON<PropTypeModel>(response.DataAsText);
            returnResponseObject?.Invoke(showroomInstanceModel);
        }).Send();
    }
    
    public static void GetList(Action<PropTypeListDto> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.PropTypes);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            PropTypeListDto propTypeListDto = JsonUtil.CreateFromJSON<PropTypeListDto>(response.DataAsText);
            returnResponseObject?.Invoke(propTypeListDto);
        }).Send();
    }
}
