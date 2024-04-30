using System;
using NaughtyAttributes;
using UnityEngine;

public class ModelPlaceholder : MonoBehaviour, IPlaceholder
{
    public event Action<IPlaceholder, ShowroomAssetModel, bool> OnUpdatePlaceholder;
    public event Action<Transform> OnAssetAssigned;

    [SerializeField] private string _modelName;

    [BoxGroup("Placeholder Model"), SerializeField]
    private GameObject _placeholderModel;

    private ShowroomAssetModel _showroomAssetModel;

    private PropTypeModel _propTypeModel;
    private PlaceholderModel _placeholder;
    private GameObject _loadedModel;
    private GameObject _assignedModel;

    #region Properties

    [field: SerializeField] public uint Id { get; set; } = 1;

    public GameObject LoadedModel
    {
        get => _loadedModel;
        set
        {
            _loadedModel = value;
            OnAssetAssigned?.Invoke(_loadedModel.transform);
        }
    }

    public PropTypeModel PropTypeModel
    {
        get => _propTypeModel;
        set => _propTypeModel = value;
    }

    public string ModelName
    {
        get => _modelName;
        set => _modelName = value;
    }

    public PlaceholderModel PlaceholderModel
    {
        get => _placeholder;
        set => _placeholder = value;
    }

    public ShowroomAssetModel ShowroomAssetModel
    {
        get => _showroomAssetModel;
        set => _showroomAssetModel = value;
    }

    public GameObject AssignedModel
    {
        get => _assignedModel;
        set => _assignedModel = value;
    }

    #endregion

    private void Awake()
    {
        return;
        PlaceholderManager.Instance.AddToDictionary(this);
        _placeholderModel.SetActive(false);
    }

    public void UpdateAsset(ShowroomAssetModel showroomAssetModel, bool updateRecordInDatabase = false)
    {
        if (_assignedModel != null) DestroyAssignedModel();
        _showroomAssetModel = showroomAssetModel;
        _modelName = showroomAssetModel.Name;
        OnUpdatePlaceholder?.Invoke(this, showroomAssetModel, updateRecordInDatabase);
    }

    private void DestroyAssignedModel()
    {
        Destroy(_assignedModel);
        _assignedModel = null;
    }
}