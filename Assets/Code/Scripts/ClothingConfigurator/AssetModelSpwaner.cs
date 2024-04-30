using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetModelSpwaner : MonoBehaviour
{
    [SerializeField] Transform _spawnPoints;

    private ClothingModelImporter _clothingModelImporter;

    private void Awake()
    {
        _clothingModelImporter = GetComponent<ClothingModelImporter>();
        _clothingModelImporter.OnActionComplete += ClothingModelImporter_OnActionComplete;
    }

    private void ClothingModelImporter_OnActionComplete()
    { 
        _clothingModelImporter.LoadedModel.transform.position = _spawnPoints.transform.position;
        _clothingModelImporter.LoadedModel.transform.rotation = _spawnPoints.transform.rotation;

    }
}
