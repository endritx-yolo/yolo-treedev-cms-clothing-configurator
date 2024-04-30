using NaughtyAttributes;
using UnityEngine;

public class TextureAssetAssigner : MonoBehaviour
{
    private TexturePlaceholder _texturePlaceholder;

    [BoxGroup("Quad Renderer"), SerializeField]
    private MeshRenderer _quadRenderer;

    private void Awake() => _texturePlaceholder = GetComponent<TexturePlaceholder>();

    private void OnEnable()
    {
        _texturePlaceholder.OnUpdatePlaceholder += TexturePlaceholder_OnUpdatePlaceholder;
        _texturePlaceholder.OnAssetAssigned += TexturePlaceholder_OnAssetAssigned;
    }

    private void OnDisable()
    {
        _texturePlaceholder.OnUpdatePlaceholder -= TexturePlaceholder_OnUpdatePlaceholder;
        _texturePlaceholder.OnAssetAssigned -= TexturePlaceholder_OnAssetAssigned;
    }

    private void TexturePlaceholder_OnUpdatePlaceholder(
        IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateAssetInDatabase)
    {
        if (placeholder is TexturePlaceholder texturePlaceholder)
        {
            string fileId = showroomAssetModel.Object;
            TextureLoaderManager textureLoaderManager =
                new TextureLoaderManager(texturePlaceholder, fileId, _quadRenderer);
            textureLoaderManager.LoadTextureFromURLAndUpdateRenderer(() =>
                OnTextureLoaded(placeholder, showroomAssetModel, updateAssetInDatabase));
        }
    }

    private void OnTextureLoaded(IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateAssetInDatabase)
    {
        if (!updateAssetInDatabase) return;
        uint showroomInstanceId = ShowroomManager.Instance.InstanceId;
        uint placeholderId = placeholder.Id;
        uint assetId = showroomAssetModel.Id;
        PlaceholderController.AddInstancePropAsset(showroomInstanceId, placeholderId, assetId);
    }

    private void TexturePlaceholder_OnAssetAssigned(Transform newAssetTransform)
    {
        // texture finished loading
    }
}