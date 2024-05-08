using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor.UIElements;
using UnityEngine;

public class MannequinInspection : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private const float InteractionDistance = 5f;


    private ClothingModelImporter _clothingModelImporter;
    private CameraInspectorEffect _cameraInspectorEffect;
    private Vector3 _cameraPosition;
    private bool _isInspectionView = false;

    private void Awake()
    {
        _clothingModelImporter = GetComponent<ClothingModelImporter>();
        _cameraInspectorEffect = FindObjectOfType<CameraInspectorEffect>();
    }

    private void Update()
    {
        var distance = Vector3.Distance(_clothingModelImporter.LoadedModel.transform.position,
            _playerTransform.transform.position);

        if (!(distance <= InteractionDistance)) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        _isInspectionView = !_isInspectionView;
                
        if (_isInspectionView)
        {
            _cameraInspectorEffect.InspectionCameraEffect();
        }
        else
        {
            _cameraInspectorEffect.ResetCameraSettings();
        }
    }


    private void InspectionMannequin()
    {
        _cameraInspectorEffect.InspectionCameraEffect();
    }
}