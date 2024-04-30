using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Video;

public class VideoAssetAssigner : MonoBehaviour
{
    private VideoPlaceholder _videoPlaceholder;

    [BoxGroup("Video"), SerializeField] private VideoPlayer _videoPlayer;

    private void Awake() => _videoPlaceholder = GetComponent<VideoPlaceholder>();

    private void OnEnable()
    {
        _videoPlaceholder.OnUpdatePlaceholder += VideoPlaceholder_OnUpdatePlaceholder;
        _videoPlaceholder.OnAssetAssigned += VideoPlaceholder_OnAssetAssigned;
    }

    private void OnDisable()
    {
        _videoPlaceholder.OnUpdatePlaceholder -= VideoPlaceholder_OnUpdatePlaceholder;
        _videoPlaceholder.OnAssetAssigned -= VideoPlaceholder_OnAssetAssigned;
    }

    private void VideoPlaceholder_OnUpdatePlaceholder(
        IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateRecordInDatabase)
    {
        if (placeholder is VideoPlaceholder videoPlaceholder)
        {
            string fileId = showroomAssetModel.Object;

            VideoLoaderManager videoLoaderManager = new VideoLoaderManager(videoPlaceholder, fileId, _videoPlayer);
            videoLoaderManager.LoadVideoFromURL(() => OnVideoLoaded(placeholder, showroomAssetModel, updateRecordInDatabase));
        }
    }

    private void OnVideoLoaded(IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateAssetInDatabase)
    {
        if (!updateAssetInDatabase) return;
        uint showroomInstanceId = ShowroomManager.Instance.InstanceId;
        uint placeholderId = placeholder.Id;
        uint assetId = showroomAssetModel.Id;
        PlaceholderController.AddInstancePropAsset(showroomInstanceId, placeholderId, assetId);
    }

    private void VideoPlaceholder_OnAssetAssigned(Transform newAssetTransform)
    {
        // video finished loading
    }
}