using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private ClothingModelImporter[] _clothingModelImporter;
    private readonly List<GameObject> _loadedModels = new();
    private readonly List<Texture> _loadedTextures = new();
    private readonly List<Color> _loadedColors = new();
    private GLTFModelImporter[] _modelImporterArray;

    private readonly string _layerMaskName = "Inspection";

    private void Awake()
    {
        _modelImporterArray = FindObjectsOfType<GLTFModelImporter>();
        _clothingModelImporter = GetComponentsInChildren<ClothingModelImporter>();

        for (int i = 0; i < _modelImporterArray.Length; i++)
        {
            _modelImporterArray[i].OnLoaded += GetTexturesAndColors;
        }
    }

    private void GetTexturesAndColors()
    {
        for (int i = 0; i < _modelImporterArray.Length; i++)
            if (!_modelImporterArray[i].IsLoaded)
                return;

        foreach (var clothingModel in _clothingModelImporter)
        {
            _loadedModels.Add(clothingModel.LoadedModel);
        }

        foreach (var loadedModel in _loadedModels)
        {
            for (var i = 0; i < loadedModel.transform.childCount; i++)
            {
                Renderer renderer = loadedModel.transform.GetChild(i).GetComponent<Renderer>();
                if (renderer == null) continue;
                var loadedModelMaterial = loadedModel.transform.GetChild(i).GetComponent<Renderer>().material;
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
                Renderer renderer = child.GetComponent<MeshRenderer>();
                if (renderer == null) continue;
                var loadedModelMaterial = child.GetComponent<MeshRenderer>().material;
                loadedModelMaterial.shader = Shader.Find("Standard");
                loadedModelMaterial.SetTexture("_MainTex", _loadedTextures[i]);
                loadedModelMaterial.SetColor("_Color", _loadedColors[i]);
            }

            var modeli = loadedModel.transform;
            modeli.gameObject.layer = LayerMask.NameToLayer(_layerMaskName);
            modeli.gameObject.tag = "Object";
        }
    }
}