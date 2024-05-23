using UnityEngine;
using Piglet;
using UriUtil = Utility.UriUtil;
using System;

public class GLTFModelImporter : MonoBehaviour
{
    public event Action OnLoaded;

    
    
    private GltfImportTask _task;
    private GameObject _model;

    private bool _isLoaded;

    [SerializeField] private float _currentProgress;
    [SerializeField] private float _totalDownload;

    public bool IsLoaded
    {
        get => _isLoaded;
        set
        {
            _isLoaded = value;
            if (_isLoaded) OnLoaded?.Invoke();
        }
    }

    public GameObject Model => _model;

    public float CurrentProgress
    {
        get => _currentProgress;
        private set => _currentProgress = value;
    }

    public float TotalDownload
    {
        get => _totalDownload;
        private set => _totalDownload = value;
    }

    private Action<GameObject> _cachedCallback;

    
    
    public void ImportModelWithName(string fileName, Action<GameObject> callback)
    {
        string requestUri = UriUtil.FormatUriUploads(fileName);
        _task = RuntimeGltfImporter.GetImportTask(requestUri);
        _cachedCallback = callback;
        _task.OnProgress = OnProgress;
        _task.OnCompleted = OnCompleted;
    }

    public void ImportModelWithUrl(string url, Action<GameObject> callback)
    {
        _task = RuntimeGltfImporter.GetImportTask(url);
        _cachedCallback = callback;
        _task.OnProgress = OnProgress;
        _task.OnCompleted = OnCompleted;
    }

    private void OnCompleted(GameObject importedModel)
    {
        _model = importedModel;
        _cachedCallback?.Invoke(importedModel);
        IsLoaded = true;
    }

    private void OnProgress(GltfImportStep step, int completed, int total)
    {
        Debug.LogFormat("{0}: {1}/{2}", step, completed, total);
        if (step != GltfImportStep.Download) return;
        CurrentProgress = completed;
        TotalDownload = total;
    }

    private void Update()
    {
        _task?.MoveNext();
    }
}