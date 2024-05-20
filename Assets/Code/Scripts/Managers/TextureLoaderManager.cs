using System;
using BestHTTP;
using UnityEngine;
using Utility;

public class TextureLoaderManager
{
    private TexturePlaceholder _texturePlaceholder;
    private string _textureName;
    private string _imageDescriptionName;
    private MeshRenderer _quadRenderer;
    private static readonly int MainTexId = Shader.PropertyToID("_MainTex");
    private MaterialPropertyBlock _materialPropertyBlock;
    private InstancePlaceholderAssetDto _instancePlaceholderAssetDto;

    public TextureLoaderManager(TexturePlaceholder texturePlaceholder, string textureName, MeshRenderer quadRenderer)
    {
        _texturePlaceholder = texturePlaceholder;
        _textureName = textureName;
        _quadRenderer = quadRenderer;
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public TextureLoaderManager(string textureName)
    {
        _textureName = textureName;
    }

    public TextureLoaderManager(string textureName,
        string imageDescriptionName,
        InstancePlaceholderAssetDto instancePlaceholderAssetDto)
    {
        _textureName = textureName;
        _imageDescriptionName = imageDescriptionName;
        _instancePlaceholderAssetDto = instancePlaceholderAssetDto;
    }

    public void LoadTextureFromURLAndUpdateRenderer(Action callback)
    {
        string requestUri = UriUtil.FormatUriUploads(_textureName);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            if (!response.IsSuccess) return;
            var texture = new Texture2D(0, 0);
            texture.LoadImage(response.Data);
            UpdateRendererMaterial(texture);
            GameObject quad = _quadRenderer.gameObject;
            quad.name = $"{quad.name} - {_textureName}";
            _texturePlaceholder.LoadedTexture = texture;
            callback?.Invoke();
        }).Send();
    }

    public void LoadTextureFromURL(Action<Texture2D, string, InstancePlaceholderAssetDto> returnObject)
    {
        string requestUri = UriUtil.FormatUriUploads(_textureName);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            if (!response.IsSuccess) return;
            var texture = new Texture2D(0, 0);
            texture.LoadImage(response.Data);
            returnObject?.Invoke(texture, _imageDescriptionName, _instancePlaceholderAssetDto);
        }).Send();
    }
    
    public void LoadTextureFromURL(Action<Texture2D> returnObject)
    {
        string requestUri = UriUtil.FormatUriUploads(_textureName);
        new HTTPRequest(new Uri(requestUri), (request, response) =>
        {
            if (!response.IsSuccess) return;
            var texture = new Texture2D(0, 0);
            texture.LoadImage(response.Data);
            returnObject?.Invoke(texture);
        }).Send();
    }

    private void UpdateRendererMaterial(Texture2D loadedTexture)
    {
        _materialPropertyBlock.SetTexture(MainTexId, loadedTexture);
        _quadRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}