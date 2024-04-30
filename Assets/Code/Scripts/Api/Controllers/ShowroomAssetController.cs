using System;
using BestHTTP;
using Utility;

public static class ShowroomAssetController
{
    #region Get

    public static void GetById(uint id, Action<ShowroomAssetModel> responseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Assets, id);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            ShowroomAssetModel showroomAssetModel = JsonUtil.CreateFromJSON<ShowroomAssetModel>(response.DataAsText);
            responseObject?.Invoke(showroomAssetModel);
        }).Send();
    }

    public static void GetList(Action<ShowroomAssetListDto> responseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Assets);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            ShowroomAssetListDto showroomAssetListDto =
                JsonUtil.CreateFromJSON<ShowroomAssetListDto>(response.DataAsText);
            responseObject?.Invoke(showroomAssetListDto);
        }).Send();
    }

    #endregion

    #region Update

    public static void PostUpdateById(ShowroomAssetModel showroomAssetModel)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Assets, showroomAssetModel.Id);
        var request = new HTTPRequest(new Uri(requestUri), methodType: HTTPMethods.Post, callback: OnRequestFinished);
    }

    private static void OnRequestFinished(HTTPRequest req, HTTPResponse resp)
    {
    }

    #endregion
}