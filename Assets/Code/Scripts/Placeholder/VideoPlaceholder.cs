using System;
using UnityEngine;

public class VideoPlaceholder : MonoBehaviour, IPlaceholder
{
    public event Action<IPlaceholder, ShowroomAssetModel, bool> OnUpdatePlaceholder;
    public event Action<Transform> OnAssetAssigned;

    [SerializeField] private string _videoName;

    private ShowroomAssetModel _showroomAssetModel;
    private PropTypeModel _propTypeModel;
    private PlaceholderModel _placeholderModel;

    #region Properties

    [field: SerializeField] public uint Id { get; set; } = 1;

    public string VideoName
    {
        get => _videoName;
        set => _videoName = value;
    }

    public ShowroomAssetModel ShowroomAssetModel
    {
        get => _showroomAssetModel;
        set => _showroomAssetModel = value;
    }

    public PlaceholderModel PlaceholderModel
    {
        get => _placeholderModel;
        set => _placeholderModel = value;
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
        _videoName = showroomAssetModel.Name;
        OnUpdatePlaceholder?.Invoke(this, showroomAssetModel, updateRecordInDatabase);
    }
}