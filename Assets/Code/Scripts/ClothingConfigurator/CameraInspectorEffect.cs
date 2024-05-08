using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Cinemachine;

public class CameraInspectorEffect : SceneSingleton<CameraInspectorEffect>
{
    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;
    [SerializeField] private LayerMask newCameraCullingMask;
    [SerializeField] private CameraClearFlags newCameraClearFlags;
    [SerializeField] private Color newCameraBackgroundColor;
    
    

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void InspectionCameraEffect()
    {
        _mainCamera.cullingMask = newCameraCullingMask;
        _mainCamera.backgroundColor = newCameraBackgroundColor;
        _mainCamera.clearFlags = newCameraClearFlags;
    }

    public void ResetCameraSettings()
    {
        _mainCamera.Reset();
    }
}