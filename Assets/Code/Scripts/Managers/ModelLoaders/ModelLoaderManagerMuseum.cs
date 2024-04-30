using System;
using TriLibCore;
using UnityEngine;
using Utility;

public class ModelLoaderManagerMuseum : ModelLoaderManager
{
    private event Action CallbackAction;

    private AssetLoaderOptions _assetLoaderOptions;
    private WaitForSeconds _wait;

    private ModelPlaceholder _modelPlaceholder;
    private ModelPlaceholderAssetAssignerMuseum _museumAssetAssigner;
    private string _modelName;
    private string _gameObjectName;

    private int _retries = 0;
    private int _totalRetries = 5;

    public ModelLoaderManagerMuseum(ModelPlaceholder modelPlaceholder, string modelName, string gameObjectName,
        ModelPlaceholderAssetAssignerMuseum museumAssetAssigner)
        : base(modelPlaceholder, modelName, gameObjectName)
    {
        _assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions(false, true);
        _modelPlaceholder = modelPlaceholder;
        _modelName = modelName;
        _gameObjectName = gameObjectName;
        _museumAssetAssigner = museumAssetAssigner;
    }

    public override void LoadModelFromURL(Action callback)
    {
        string requestUri = UriUtil.FormatUriUploads(_modelName);
        var webRequest = AssetDownloader.CreateWebRequest(requestUri);
        AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null,
            _assetLoaderOptions);
        CallbackAction = callback;
    }

    protected override void OnLoad(AssetLoaderContext assetLoaderContext)
    {
        GameObject newGameObject = assetLoaderContext.RootGameObject;
        if (_modelPlaceholder != null)
        {
            _modelPlaceholder.LoadedModel = newGameObject;
            newGameObject.name = _gameObjectName;
            _modelPlaceholder.AssignedModel = newGameObject;
        }
        else
        {
            newGameObject.name = _gameObjectName;
        }

        CallbackAction?.Invoke();
    }

    protected override void OnError(IContextualizedError obj)
    {
        _retries++;
        if (_retries >= _totalRetries) return;
        LoadModelFromURL(CallbackAction);
    }
}