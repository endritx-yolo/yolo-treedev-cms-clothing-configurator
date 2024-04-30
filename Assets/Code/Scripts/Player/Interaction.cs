using System;
using HighlightPlus;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public static event Action<PlaceholderType> OnAnyInteractWithPlaceholder;
    public static event Action<IPlaceholder> OnAnyStorePlaceholder;
    public static event Action OnAnyCancelInput;
    public static event Action<float> OnAnyUpdateInteractionRayLength;

    [SerializeField] private LayerMask _interactablesMask;
    [SerializeField] private float _rayLength = 3.5f;
    [SerializeField] private float _rayLengthFPS = 3.5f;
    [SerializeField] private float _rayLengthTPS = 6f;

    private bool _canInteract = true;

    private Camera _mainCamera;
    private PlayerInputHandler _playerInput;
    private HighlightEffect _lookAtPlaceholderHighlight;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _playerInput = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
        UIAssetItemsPresenter.OnAnyOpenMenuCanvas += UIAssetItemsPresenter_OnAnyOpenMenuCanvas;
        UIAssetItemsPresenter.OnAnyCloseMenuCanvas += UIAssetItemsPresenter_OnAnyDisableMenuCanvas;
        PointOfView.OnAnyTogglePointOfView += UpdateRayLength;
    }

    private void OnDisable()
    {
        UIAssetItemsPresenter.OnAnyOpenMenuCanvas -= UIAssetItemsPresenter_OnAnyOpenMenuCanvas;
        UIAssetItemsPresenter.OnAnyCloseMenuCanvas -= UIAssetItemsPresenter_OnAnyDisableMenuCanvas;
        PointOfView.OnAnyTogglePointOfView -= UpdateRayLength;
    }

    private void Update()
    {
        HandleCancelInput();
        if (!_canInteract) return;
        HandleInteraction();
    }

    private void UIAssetItemsPresenter_OnAnyOpenMenuCanvas() => _canInteract = false;

    private void UIAssetItemsPresenter_OnAnyDisableMenuCanvas() => _canInteract = true;

    private void HandleInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.TransformDirection(Vector3.forward),
                out hit, _rayLength, _interactablesMask))
        {
            if (!_playerInput.interact) return;

            if (hit.transform.TryGetComponent(out PlaceholderReference placeholderReference))
            {
                OnAnyInteractWithPlaceholder?.Invoke(placeholderReference.PlaceholderType);
                OnAnyStorePlaceholder?.Invoke(placeholderReference.Placeholder);
            }

            Debug.DrawRay(_mainCamera.transform.position,
                _mainCamera.transform.TransformDirection(Vector3.forward) * _rayLength, Color.green);
        }
        else
        {
            Debug.DrawRay(_mainCamera.transform.position,
                _mainCamera.transform.TransformDirection(Vector3.forward) * _rayLength, Color.red);
        }

        _playerInput.interact = false;
    }

    private void HandleCancelInput()
    {
        if (!_playerInput.cancel) return;

        OnAnyCancelInput?.Invoke();
        _playerInput.cancel = false;
    }

    private void UpdateRayLength(PointOfViewType pointOfViewType)
    {
        switch (pointOfViewType)
        {
            case PointOfViewType.FPS:
                _rayLength = _rayLengthFPS;
                break;
            case PointOfViewType.TPS:
                _rayLength = _rayLengthTPS;
                break;
        }
        
        OnAnyUpdateInteractionRayLength?.Invoke(_rayLength);
    }
}