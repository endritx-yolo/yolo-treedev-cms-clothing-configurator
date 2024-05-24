using NaughtyAttributes;
using UnityEngine;
using System;

public class LoadingScreenCanvasManager : MonoBehaviour
{
    public static event Action OnAnyLoadingFinished;
    
    [BoxGroup("Loading Screen Panel")]
    [SerializeField] private GameObject loadingScreenCanvas;
    
    private SliderPresenter _sliderPresenter;

    private GLTFModelImporter[] _gltfModelImporter;
    private GltfImporterTotalCalculation[] _gltfImporterTotalCalculations;
    
    private void Awake()
    {
        _gltfImporterTotalCalculations = FindObjectsOfType<GltfImporterTotalCalculation>();
        _gltfModelImporter = FindObjectsOfType<GLTFModelImporter>();
        _sliderPresenter = GetComponent<SliderPresenter>();
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
        _sliderPresenter.UpdateSlider(currentDownload, totalDownload);
    }

    private void TryHideLoadingScreen()
    {
        foreach (var model in _gltfModelImporter)
            if (!model.IsLoaded)
                return;

        OnAnyLoadingFinished?.Invoke();
    }
}
