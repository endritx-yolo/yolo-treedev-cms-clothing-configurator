using System;
using UnityEngine;

public class ClothingModelImporter : MonoBehaviour
{
    [SerializeField] private uint _clothingAssetId = 68;
    [SerializeField] private string _fileName = "jevnBOd2TOHV5lxDEmTS.zip";

    private ClothingModelLoaderManager _loadingManager;
    
    private void Awake()
    {
        _loadingManager = GetComponent<ClothingModelLoaderManager>();
        ShowroomAssetController.GetById(_clothingAssetId, OnGetAssetModel);
    }

    private void OnGetAssetModel(ShowroomAssetModel assetModel)
    {
        _loadingManager.LoadModelFromUrl(assetModel.Object);
    }
}