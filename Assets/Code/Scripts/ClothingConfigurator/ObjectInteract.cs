using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
   [SerializeField] private ClothingModelImporter[] _clothingModelImporter;
   [SerializeField] private readonly List<GameObject> _loadedModels = new();

    private void Awake()
    {
        _clothingModelImporter = GetComponentsInChildren<ClothingModelImporter>();
        StartCoroutine(FillListWithLoadedModels());
    }

    private IEnumerator FillListWithLoadedModels()
    {
        yield return new WaitForSeconds(5f);
        foreach (var clothingModel in _clothingModelImporter)
        {
            _loadedModels.Add(clothingModel.LoadedModel);
        }
        AddBoxColliderToModels();
    }

    private void AddBoxColliderToModels()
    {
        foreach (var model in _loadedModels)
        {
            model.AddComponent<MannequinInspection>();
            var boxCollider = model.AddComponent<BoxCollider>();
            boxCollider.size = Vector3.one * 3f;
            boxCollider.isTrigger = true;

        }
    }
    
}
