using System;
using BestHTTP;
using UnityEngine;
using Utility;

public static class PlaceholderController
{
    #region Get

    public static void GetById(uint id, Action<PlaceholderModel> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Props, id);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            PlaceholderModel placeholderModel = JsonUtil.CreateFromJSON<PlaceholderModel>(response.DataAsText);
            returnResponseObject?.Invoke(placeholderModel);
        }).Send();
    }

    public static void GetList(Action<PlaceholderListDto> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Props);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            PlaceholderListDto placeholderListDto = JsonUtil.CreateFromJSON<PlaceholderListDto>(response.DataAsText);
            returnResponseObject?.Invoke(placeholderListDto);
        }).Send();
    }

    #endregion

    #region Update

    public static void AddInstancePropAsset(uint showroomInstanceId, uint placeholderId, uint assetId)
    {
        string accessToken = LoginManager.Instance.AccessToken;
        string requestUri = UriUtil.FormatUriApi(Config.AddPropAsset, showroomInstanceId, placeholderId, assetId);
        var request = new HTTPRequest(new Uri(requestUri), methodType: HTTPMethods.Post);
        
        request.AddHeader("Authorization", $"Bearer {accessToken}");
        request.Send();
    }

    #endregion
}