using System;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake() => _renderer = GetComponent<Renderer>();

    private void OnEnable() => PointOfView.OnAnyTogglePointOfView += UpdateMesh;
    private void OnDisable() => PointOfView.OnAnyTogglePointOfView -= UpdateMesh;

    private void UpdateMesh(PointOfViewType pointOfViewType) =>
        _renderer.enabled = pointOfViewType == PointOfViewType.TPS;
}