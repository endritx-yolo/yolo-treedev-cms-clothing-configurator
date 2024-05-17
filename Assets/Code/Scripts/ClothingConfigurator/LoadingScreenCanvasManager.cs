using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenCanvasManager : MonoBehaviour
{
    
    [SerializeField] private GameObject loadingScreenCanvas;
    private GLTFModelImporter[] _gltfModelImporter;
    [SerializeField] private SliderPresenter sliderPresenter;
    [SerializeField] private SliderPresenter highlitedSliderPresenter;
    
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
        highlitedSliderPresenter.UpdateSlider(currentDownload, totalDownload);
    }

    private void TryHideLoadingScreen()
    {
        foreach (var model in _gltfModelImporter)
            if (!model.IsLoaded)
                return;

        loadingScreenCanvas.SetActive(false);
    }
   
    
}
