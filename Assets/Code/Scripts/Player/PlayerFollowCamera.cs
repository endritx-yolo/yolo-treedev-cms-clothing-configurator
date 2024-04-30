using Cinemachine;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private PointOfViewType _pointOfViewType;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.enabled = false;
    }

    private void OnEnable() => PointOfView.OnAnyTogglePointOfView += UpdateVirtualCameraState;
    private void OnDisable() => PointOfView.OnAnyTogglePointOfView -= UpdateVirtualCameraState;

    private void UpdateVirtualCameraState(PointOfViewType poinOfView) =>
        _cinemachineVirtualCamera.enabled = _pointOfViewType == poinOfView;
}