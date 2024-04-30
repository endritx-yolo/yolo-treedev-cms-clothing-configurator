using System;
using NaughtyAttributes;
using UnityEngine;

public class ClothingModelImporter : MonoBehaviour
{
    public event Action OnActionComplete;
    
    [SerializeField] private uint _clothingAssetId;

    [SerializeField] private GameObject _loadedModel;
    
    private GLTFModelImporter _gltfImporter;

    public GameObject LoadedModel => _loadedModel;
    
    [Button]
    private void Start()
    {
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
       OnActionComplete?.Invoke();
    }
}