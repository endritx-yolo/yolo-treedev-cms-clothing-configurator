using System;
using BestHTTP;
using Utility;

public static class ShowroomController
{
    public static void GetBySlug(string slug, Action<ShowroomModel> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Showrooms, slug);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            ShowroomModel showroomModel = JsonUtil.CreateFromJSON<ShowroomModel>(response.DataAsText);
            returnResponseObject?.Invoke(showroomModel);
        }).Send();
    }
    
    public static void GetList(Action<ShowroomListDto> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Showrooms);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            ShowroomListDto showroomListDto = JsonUtil.CreateFromJSON<ShowroomListDto>(response.DataAsText);
            returnResponseObject?.Invoke(showroomListDto);
        }).Send();
    }
}