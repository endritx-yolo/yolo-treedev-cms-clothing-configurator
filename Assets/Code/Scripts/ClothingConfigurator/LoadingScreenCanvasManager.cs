using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LoadingScreenCanvasManager : MonoBehaviour
{
    [BoxGroup("Loading Screen Panel")]
    [SerializeField] private GameObject loadingScreenCanvas;
    [BoxGroup("Sliders")]
    [SerializeField] private SliderPresenter sliderPresenter;
    [BoxGroup("Sliders")]
    [SerializeField] private SliderPresenter highlitedSliderPresenter;
    
    
    private GLTFModelImporter[] _gltfModelImporter;
    private GltfImporterTotalCalculation[] _gltfImporterTotalCalculations;
    
    private void Awake()
    {
        _gltfImporterTotalCalculations = FindObjectsOfType<GltfImporterTotalCalculation>();
        _gltfModelImporter = FindObjectsOfType<GLTFModelImporter>();
        foreach (var model in _gltfModelImporter)
        {
            model.OnLoaded += TryHideLoadingScreen;
        }
    }
    
    private void OnEnable()
    {
        GltfImporterTotalCalculation.OnLoadingModels += GLTFModelImporterOnOnLoadingModels;
    }
    
    private void OnDisable()
    {
        GltfImporterTotalCalculation.OnLoadingModels -= GLTFModelImporterOnOnLoadingModels;
    }
    
    private void GLTFModelImporterOnOnLoadingModels(float currentDownload, float totalDownload)
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
