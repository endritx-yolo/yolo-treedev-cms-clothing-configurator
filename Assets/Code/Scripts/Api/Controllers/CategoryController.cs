using System;
using BestHTTP;
using Utility;

public static class CategoryController
{
    public static void GetById(uint id, Action<CategoryModel> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Categories, id);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            CategoryModel categoryModel = JsonUtil.CreateFromJSON<CategoryModel>(response.DataAsText);
            returnResponseObject?.Invoke(categoryModel);
        }).Send();
    }
    
    public static void GetList(Action<CategoryListDto> returnResponseObject)
    {
        string requestUri = UriUtil.FormatUriApi(Config.Categories);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            CategoryListDto categoryListDto = JsonUtil.CreateFromJSON<CategoryListDto>(response.DataAsText);
            returnResponseObject?.Invoke(categoryListDto);
        }).Send();
    }
}
