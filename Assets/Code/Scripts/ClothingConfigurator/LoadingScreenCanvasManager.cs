using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenCanvasManager : MonoBehaviour
{
    
    [SerializeField] private GameObject loadingScreenCanvas;
    private GLTFModelImporter[] _gltfModelImporter;
    [SerializeField] private SliderPresenter sliderPresenter;
    
    private void Awake()
    {
        _gltfModelImporter = FindObjectsOfType<GLTFModelImporter>();
        foreach (var model in _gltfModelImporter)
        {
            model.OnLoaded += TryHideLoadingScreen;
        }
    }
    
    private void OnEnable()
    {
        GLTFModelImporter.OnLoadingModels += GLTFModelImporterOnOnLoadingModels;
    }
    
    private void OnDisable()
    {
        GLTFModelImporter.OnLoadingModels -= GLTFModelImporterOnOnLoadingModels;
    }
    
    private void GLTFModelImporterOnOnLoadingModels(int currentDownload, int totalDownload)
    {
        loadingScreenCanvas.SetActive(true);
        sliderPresenter.UpdateSlider(currentDownload, totalDownload);
        Debug.Log("CURRENT: " + currentDownload);

    }

    private void TryHideLoadingScreen()
    {
        for (int i = 0; i < _gltfModelImporter.Length; i++)
            if (!_gltfModelImporter[i].IsLoaded)
                return;
        
        loadingScreenCanvas.SetActive(false);
    }
   
    
}
