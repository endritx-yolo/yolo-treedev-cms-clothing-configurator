using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class LoadSceneManager : MonoBehaviour
{
    [Scene][SerializeField] private string sceneToLoadName;
    private GLTFModelImporter[] _modelImporterArray;
    [SerializeField] private GameObject mainCamera;
    

   

    private void Awake()
    {
        mainCamera.SetActive(true);
        _modelImporterArray = FindObjectsOfType<GLTFModelImporter>();

        for (int i = 0; i < _modelImporterArray.Length; i++)
        {
            _modelImporterArray[i].OnLoaded += TryLoadNextScene;

        }
    }

    private void TryLoadNextScene()
    {
        for (int i = 0; i < _modelImporterArray.Length; i++)
            if (!_modelImporterArray[i].IsLoaded)
                return;

        mainCamera.SetActive(false);
        SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
    }
}