using System;
using TriLibCore;
using UnityEngine;
using Utility;

public class ModelLoaderManager
{
    private event Action CallbackAction;
    
    private AssetLoaderOptions _assetLoaderOptions;
    private WaitForSeconds _wait;

    private ModelPlaceholder _modelPlaceholder;
    private string _modelName;
    private string _gameObjectName;

    public ModelLoaderManager(ModelPlaceholder modelPlaceholder, string modelName, string gameObjectName)
    {
        _assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions(false, true);
        _modelPlaceholder = modelPlaceholder;
        _modelName = modelName;
        _gameObjectName = gameObjectName;
    }

    public virtual void LoadModelFromURL(Action callback)
    {
        string requestUri = UriUtil.FormatUriUploads(_modelName);
        var webRequest = AssetDownloader.CreateWebRequest(requestUri);
        AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null,
            _assetLoaderOptions);
        CallbackAction = callback;
    }

    protected virtual void OnError(IContextualizedError obj)
    {
        Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
    }

    protected virtual void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
    {
        //Debug.Log($"Loading Model. Progress: {progress:P}");
    }

    protected virtual void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
    {

    }

    protected virtual void OnLoad(AssetLoaderContext assetLoaderContext)
    {
        GameObject newGameObject = assetLoaderContext.RootGameObject;
        _modelPlaceholder.LoadedModel = newGameObject;
        newGameObject.name = _gameObjectName;
        CallbackAction?.Invoke();
    }
}