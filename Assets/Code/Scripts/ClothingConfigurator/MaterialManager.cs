using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TriLibCore.Dae.Schema;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private ClothingModelImporter[] _clothingModelImporter;
    private readonly List<GameObject> _loadedModels = new();
    private readonly List<Texture> _loadedTextures = new();
    private readonly List<Color> _loadedColors = new();
    
    private readonly string _layerMaskName = "Inspection";

    private void Awake()
    {
        _clothingModelImporter = GetComponentsInChildren<ClothingModelImporter>();
        StartCoroutine(GetTexturesAndColors());
    }

    private IEnumerator GetTexturesAndColors()
    {
        yield return new WaitForSeconds(7f);
        foreach (var clothingModel in _clothingModelImporter)
        {
            _loadedModels.Add(clothingModel.LoadedModel);
        }
        foreach (var loadedModel in _loadedModels)
        {
            for (var i = 0; i < loadedModel.transform.childCount; i++)
            { 
                var loadedModelMaterial = loadedModel.transform.GetChild(i).GetComponent<MeshRenderer>().material;
                var textures = loadedModelMaterial.GetTexture("_baseColorTexture");
               var colors = loadedModelMaterial.GetColor("_baseColorFactor");
               _loadedTextures.Add(textures);
               _loadedColors.Add(colors);
             
            }
        }
        ChangeShaderAndSetTexture();
    }
    
    private void ChangeShaderAndSetTexture()
    {
        foreach (var loadedModel in _loadedModels)
        {
            for (var i = 0; i < loadedModel.transform.childCount; i++)
            { 
                var child = loadedModel.transform.GetChild(i); 
                child.gameObject.layer = LayerMask.NameToLayer(_layerMaskName);
               var loadedModelMaterial = child.GetComponent<MeshRenderer>().material;
               loadedModelMaterial.shader = Shader.Find("Standard");
               loadedModelMaterial.SetTexture("_MainTex",_loadedTextures[i]);
               loadedModelMaterial.SetColor("_Color",_loadedColors[i]);
            }

            var modeli = loadedModel.transform;
            modeli.gameObject.layer = LayerMask.NameToLayer(_layerMaskName);
            modeli.gameObject.tag = "Object";
        }
    }
}
