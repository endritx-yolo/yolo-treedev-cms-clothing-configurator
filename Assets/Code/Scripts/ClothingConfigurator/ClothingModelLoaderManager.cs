using TriLibCore;
using TriLibCore.Extensions;
using TriLibCore.General;
using TriLibCore.Mappers;
using TriLibCore.Samples;
using UnityEngine;
using Utility;

public class ClothingModelLoaderManager : MonoBehaviour
{
    [SerializeField]
    private HumanoidAvatarMapper _humanoidAvatarMapper;

    private AssetLoaderOptions _assetLoaderOptions;

    private void Start()
    {
        _assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions(false, true);
    }

    public void LoadModelFromUrl(string fileName)
    {
        string requestUri = UriUtil.FormatUriUploads(fileName);
        var webRequest = AssetDownloader.CreateWebRequest(requestUri);

        Debug.Log(requestUri);
        
        AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, _assetLoaderOptions);
    }
    
    private void OnError(IContextualizedError obj)
    {
        Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
    }
    
    private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
    {
        Debug.Log($"Loading Model. Progress: {progress:P}");
    }
    
    private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
    {
    }
    
    private void OnLoad(AssetLoaderContext assetLoaderContext)
    {
        Debug.Log("Model loaded. Loading materials.");
    }
}