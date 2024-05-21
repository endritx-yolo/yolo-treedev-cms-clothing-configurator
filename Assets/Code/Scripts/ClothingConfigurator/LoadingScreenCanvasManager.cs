using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LoadingScreenCanvasManager : MonoBehaviour
{
    [BoxGroup("Loading Screen Panel")]
    [SerializeField] private GameObject loadingScreenCanvas;
    private GLTFModelImporter[] _gltfModelImporter;
    private GLTFImporterTotalCalculation[] _gltfImpoorterTotalCalculations;
    [BoxGroup("Sliders")]
    [SerializeField] private SliderPresenter sliderPresenter;
    [BoxGroup("Sliders")]
    [SerializeField] private SliderPresenter highlitedSliderPresenter;
    
    private void Awake()
    {
        _gltfImpoorterTotalCalculations = FindObjectsOfType<GLTFImporterTotalCalculation>();
        _gltfModelImporter = FindObjectsOfType<GLTFModelImporter>();
        foreach (var model in _gltfModelImporter)
        {
            model.OnLoaded += TryHideLoadingScreen;
        }
    }
    
    private void OnEnable()
    {
        GLTFImporterTotalCalculation.OnLoadingModels += GLTFModelImporterOnOnLoadingModels;
    }
    
    private void OnDisable()
    {
        GLTFImporterTotalCalculation.OnLoadingModels -= GLTFModelImporterOnOnLoadingModels;
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
