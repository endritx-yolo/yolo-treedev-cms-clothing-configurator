using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] private ClothingModelImporter[] _clothingModelImporter;
    private readonly List<GameObject> _loadedModels = new();

    private GLTFModelImporter[] _modelImporterArray;

    public List<GameObject> LoadedModels => _loadedModels;

    public ClothingModelImporter[] ClothingModelImporter => _clothingModelImporter;

    private void Awake()
    {
        _modelImporterArray = FindObjectsOfType<GLTFModelImporter>();
        _clothingModelImporter = GetComponentsInChildren<ClothingModelImporter>();
        
        for (int i = 0; i < _modelImporterArray.Length; i++)
        {
            _modelImporterArray[i].OnLoaded += ClothingModelArray;
        }
    }

    private void ClothingModelArray()
    {
        for (int i = 0; i < _modelImporterArray.Length; i++)
            if (!_modelImporterArray[i].IsLoaded)
                return;
        foreach (var clothingModel in ClothingModelImporter)
        {
            LoadedModels.Add(clothingModel.LoadedModel);
        }
        AddBoxColliderToModels();
    }

    private void AddBoxColliderToModels()
    {
        foreach (var model in LoadedModels)
        {
            model.AddComponent<MannequinInspection>();
            var boxCollider = model.AddComponent<BoxCollider>();
            boxCollider.size = Vector3.one * 3f;
            boxCollider.isTrigger = true;
        }
    }
}
