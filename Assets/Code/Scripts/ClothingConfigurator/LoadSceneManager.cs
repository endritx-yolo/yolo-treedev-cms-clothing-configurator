using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoadName;

    private GLTFModelImporter[] _modelImporterArray;

    private void Awake()
    {
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

        SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
    }
}