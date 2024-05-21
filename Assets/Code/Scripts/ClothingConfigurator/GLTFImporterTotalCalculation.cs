using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GltfImporterTotalCalculation : MonoBehaviour
{
    private GLTFModelImporter[] _modelImporterArray;
    public static event Action<float, float> OnLoadingModels; 
    
    
    private void Awake()
    {
        _modelImporterArray = FindObjectsOfType<GLTFModelImporter>();
        for (var i = 0; i < _modelImporterArray.Length; i++)
        {
            _modelImporterArray[i].OnLoaded += CalculateTotalProgress;
        }
    }

    private float _totalProgress;
    private float _currentProgress;
    

    private void CalculateTotalProgress()
    {
        for (var i = 0; i < _modelImporterArray.Length; i++)
        {
            _totalProgress += _modelImporterArray[i].TotalDownload;
            _currentProgress += _modelImporterArray[i].CurrentProgress;
        }
        OnLoadingModels?.Invoke(_currentProgress, _totalProgress);
        
    }
    
    
    
}
