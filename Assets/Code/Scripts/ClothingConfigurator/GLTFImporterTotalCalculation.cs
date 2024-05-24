using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GltfImporterTotalCalculation : MonoBehaviour
{
    [SerializeField] private GLTFModelImporter[] _modelImporterArray;
    public static event Action<float, float> OnLoadingModels;

    [SerializeField] private long _totalProgress;
    [SerializeField] private long _currentProgress;
    
    private void Awake()
    {
        _modelImporterArray = FindObjectsOfType<GLTFModelImporter>();
    }

    private void OnEnable()
    {
        for (var i = 0; i < _modelImporterArray.Length; i++)
            _modelImporterArray[i].OnDownloading += CalculateTotalProgress;
    }

    private void CalculateTotalProgress()
    {
        _currentProgress = 0;
        _totalProgress = 0;
        
        for (var i = 0; i < _modelImporterArray.Length; i++)
        {
            _totalProgress += _modelImporterArray[i].TotalDownload;
            _currentProgress += _modelImporterArray[i].CurrentProgress;
        }

        OnLoadingModels?.Invoke(_currentProgress, _totalProgress);
    }
}