using System;
using System.Collections.Generic;
using System.Linq;
using BestHTTP;
using BestHTTP.Cookies;
using UnityEngine;
using Utility;

public static class ShowroomInstanceController
{
    public static void GetById(uint id, Action<ShowroomInstanceModel> responseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.ShowroomInstances, id);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            ShowroomInstanceModel showroomInstanceModel =
                JsonUtil.CreateFromJSON<ShowroomInstanceModel>(response.DataAsText);
            responseObject?.Invoke(showroomInstanceModel);
        }).Send();
    }

    public static void GetList(Action<ShowroomInstanceListDto> responseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.ShowroomInstances);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            ShowroomInstanceListDto showroomInstanceListDto =
                JsonUtil.CreateFromJSON<ShowroomInstanceListDto>(response.DataAsText);
            responseObject?.Invoke(showroomInstanceListDto);
        }).Send();
    }

    public static void GetPlaceholderListById(uint id, string bearerToken,
        Action<List<InstancePlaceholderAssetDto>> responseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.ShowroomInstancePlaceholderAssets, id);
        HTTPRequest request = new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            List<InstancePlaceholderAssetDto> instancePlaceholderAsset =
                JsonUtil.CreateArrayFromJSONList<InstancePlaceholderAssetDto>(response.DataAsText);
            responseObject?.Invoke(instancePlaceholderAsset);
        });

        request.AddHeader("Authorization", $"Bearer {bearerToken}");
        request.Send();
    }

    //todo sort and pagination
    public static void GetPublicTextureList(uint id, string bearerToken,
        Action<List<InstancePlaceholderAssetDto>> responseObject) =>
        CreateRequest(id, bearerToken, responseObject, "Texture");

    //todo sort and pagination
    public static void GetPublicVideoList(uint id, string bearerToken,
        Action<List<InstancePlaceholderAssetDto>> responseObject) =>
        CreateRequest(id, bearerToken, responseObject, "Video");

    //todo sort and pagination
    public static void GetPublicModelList(uint id, string bearerToken,
        Action<List<InstancePlaceholderAssetDto>> responseObject) =>
        CreateRequest(id, bearerToken, responseObject, "Model");

    public static void CreateRequest(uint id, string bearerToken,
        Action<List<InstancePlaceholderAssetDto>> responseObject, string placeholderType)
    {
        string requestUri = UriUtil.FormatUriApi(Config.ShowroomInstancePlaceholderAssets, id);

        HTTPRequest request = new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            List<InstancePlaceholderAssetDto> instancePlaceholderAssetList =
                JsonUtil.CreateArrayFromJSONList<InstancePlaceholderAssetDto>(response.DataAsText);

            instancePlaceholderAssetList = instancePlaceholderAssetList.Where(x =>
                    x.ShowroomAssetModel.Category.Id == ShowroomManager.Instance.InstanceId &&
                    x.ShowroomAssetModel.Status == 1 && x.ShowroomAssetModel.PropType.Name.Equals(placeholderType))
                .ToList();

            responseObject?.Invoke(instancePlaceholderAssetList);
        });

        request.AddHeader("Authorization", $"Bearer {bearerToken}");
        request.Send();
    }
}