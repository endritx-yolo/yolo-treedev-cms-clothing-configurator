using System;
using UnityEngine;

public class TexturePlaceholder : MonoBehaviour, IPlaceholder
{
    public event Action<IPlaceholder, ShowroomAssetModel, bool> OnUpdatePlaceholder;
    public event Action<Transform> OnAssetAssigned;

    [SerializeField] private string _textureName;

    private ShowroomAssetModel _showroomAssetModel;
    private PropTypeModel _propTypeModel;
    private PlaceholderModel _placeholder;
    private Texture2D _loadedTexture;

    #region Properties

    [field: SerializeField] public uint Id { get; set; } = 1;

    public ShowroomAssetModel ShowroomAssetModel
    {
        get => _showroomAssetModel;
        set => _showroomAssetModel = value;
    }

    public Texture2D LoadedTexture
    {
        get => _loadedTexture;
        set
        {
            _loadedTexture = value;
            OnAssetAssigned?.Invoke(null);
        }
    }

    public PlaceholderModel PlaceholderModel
    {
        get => _placeholder;
        set => _placeholder = value;
    }

    public string TextureName
    {
        get => _textureName;
        set => _textureName = value;
    }

    public PropTypeModel PropTypeModel
    {
        get => _propTypeModel;
        set => _propTypeModel = value;
    }

    #endregion

    private void Awake()
    {
        return;
        PlaceholderManager.Instance.AddToDictionary(this);
    }

    public void UpdateAsset(ShowroomAssetModel showroomAssetModel, bool updateRecordInDatabase = false)
    {
        _showroomAssetModel = showroomAssetModel;
        _textureName = showroomAssetModel.Name;
        OnUpdatePlaceholder?.Invoke(this, showroomAssetModel, updateRecordInDatabase);
    }
}