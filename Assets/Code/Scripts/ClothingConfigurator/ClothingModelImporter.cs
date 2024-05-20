using System;
using NaughtyAttributes;
using UnityEngine;

public class ClothingModelImporter : MonoBehaviour
{
    public event Action OnActionComplete;
    
    [SerializeField] private uint _clothingAssetId;

    [SerializeField] private GameObject _loadedModel;
    [SerializeField] private Texture2D _clothingTexture;
    [SerializeField] private string _name;
    
    private GLTFModelImporter _gltfImporter;

    public GameObject LoadedModel => _loadedModel;

    public Texture2D ClothingTexture => _clothingTexture;

    public string Name => _name;

    [Button]
    private void Start()
    {
        _gltfImporter = GetComponent<GLTFModelImporter>();
        ShowroomAssetController.GetById(_clothingAssetId, OnGetAssetModel);
    }

    private void OnGetAssetModel(ShowroomAssetModel assetModel)
    {
        _gltfImporter.ImportModelWithName(assetModel.Object, GetModelReference);
        
        string imageName = assetModel.FileName;
        _name = assetModel.Name;
        TextureLoaderManager textureLoaderManager = new TextureLoaderManager(imageName);
        textureLoaderManager.LoadTextureFromURL(OnLoadTexture);
    }

    private void GetModelReference(GameObject model)
    {
        _loadedModel = _gltfImporter.Model;
       OnActionComplete?.Invoke();
    }

    private void OnLoadTexture(Texture2D loadedTexture)
    {
        _clothingTexture = loadedTexture;
    }
}