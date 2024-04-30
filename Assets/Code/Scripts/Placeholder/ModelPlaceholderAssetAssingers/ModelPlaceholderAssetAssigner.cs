using UnityEngine;

public class ModelPlaceholderAssetAssigner : MonoBehaviour
{
    [SerializeField] private GameObject _assetParent;

    private ModelPlaceholder _modelPlaceholder;

    protected virtual void Awake() => _modelPlaceholder = GetComponentInParent<ModelPlaceholder>();

    protected virtual void OnEnable()
    {
        _modelPlaceholder.OnUpdatePlaceholder += ModelPlaceholder_OnUpdatePlaceholder;
        _modelPlaceholder.OnAssetAssigned += ModelPlaceholder_OnAssetAssigned;
    }

    protected virtual void OnDisable()
    {
        _modelPlaceholder.OnUpdatePlaceholder -= ModelPlaceholder_OnUpdatePlaceholder;
        _modelPlaceholder.OnAssetAssigned -= ModelPlaceholder_OnAssetAssigned;
    }

    protected virtual void ModelPlaceholder_OnUpdatePlaceholder(
        IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateRecordInDatabase)
    {
        if (placeholder is ModelPlaceholder modelPlaceholder)
        {
            string fileId = showroomAssetModel.Object;
            ModelLoaderManager modelLoaderManager =
                new ModelLoaderManager(modelPlaceholder, fileId, showroomAssetModel.Name);

            modelLoaderManager.LoadModelFromURL(() =>
                OnModelLoaded(placeholder, showroomAssetModel, updateRecordInDatabase));
        }
    }

    protected void OnModelLoaded(IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateAssetInDatabase)
    {
        if (!updateAssetInDatabase) return;
        uint showroomInstanceId = ShowroomManager.Instance.InstanceId;
        uint placeholderId = placeholder.Id;
        uint assetId = showroomAssetModel.Id;
        PlaceholderController.AddInstancePropAsset(showroomInstanceId, placeholderId, assetId);
    }

    protected virtual void ModelPlaceholder_OnAssetAssigned(Transform newAssetTransform)
    {
        newAssetTransform.parent = _assetParent.transform;
        newAssetTransform.localPosition = Vector3.zero;
    }
}