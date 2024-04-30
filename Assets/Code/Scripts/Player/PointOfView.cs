using System;
using UnityEngine;

public class PointOfView : MonoBehaviour
{
    public static event Action<PointOfViewType> OnAnyTogglePointOfView;

    [SerializeField] private PointOfViewType _pointOfViewType = PointOfViewType.TPS;

    private PlayerInputHandler _playerInput;

    private void Awake() => _playerInput = GetComponent<PlayerInputHandler>();
    private void Start() => OnAnyTogglePointOfView?.Invoke(_pointOfViewType);
    private void Update() => HandleToggleView();

    private void HandleToggleView()
    {
        if (!_playerInput.toggleView) return;
        ToggleView();
        _playerInput.toggleView = false;
    }

    private void ToggleView()
    {
        if (_pointOfViewType == PointOfViewType.FPS) _pointOfViewType = PointOfViewType.TPS;
        else if (_pointOfViewType == PointOfViewType.TPS) _pointOfViewType = PointOfViewType.FPS;
        OnAnyTogglePointOfView?.Invoke(_pointOfViewType);
    }
}