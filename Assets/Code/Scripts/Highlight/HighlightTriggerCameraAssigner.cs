using HighlightPlus;
using UnityEngine;

public class HighlightTriggerCameraAssigner : MonoBehaviour
{
    private Camera _mainCamera;
    private HighlightTrigger _highlightTrigger;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _highlightTrigger = GetComponent<HighlightTrigger>();
    }

    private void Start()
    {
        if (_highlightTrigger == null) return;
        if (_mainCamera == null) return;
        if (_highlightTrigger.raycastCamera != null) return;
        _highlightTrigger.raycastCamera = _mainCamera;
    }
}
