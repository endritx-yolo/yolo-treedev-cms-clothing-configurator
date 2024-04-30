using System;
using NaughtyAttributes;
using UnityEngine;

public class ClothingModelImporter : MonoBehaviour
{
    [SerializeField] private uint _clothingAssetId = 68;

    [SerializeField] private GameObject _loadedModel;

    private ClothingModelLoaderManager _loadingManager;
    private GLTFModelImporter _gltfImporter;

    public GameObject LoadedModel => _loadedModel;

    [Button]
    private void Start()
    {
        _loadingManager = GetComponent<ClothingModelLoaderManager>();
        _gltfImporter = GetComponent<GLTFModelImporter>();
        ShowroomAssetController.GetById(_clothingAssetId, OnGetAssetModel);
    }

    private void OnGetAssetModel(ShowroomAssetModel assetModel)
    {
        _gltfImporter.ImportModelWithName(assetModel.Object, GetModelReference);
    }

    private void GetModelReference(GameObject model)
    {
        _loadedModel = _gltfImporter.Model;
    }
}