using UnityEngine;
using Piglet;
using UriUtil = Utility.UriUtil;
using System;

public class GLTFModelImporter : MonoBehaviour
{
    public event Action OnLoaded;
    public event Action OnDownloading;

    private GltfImportTask _task;
    private GameObject _model;

    private bool _isLoaded;

    private int _currentProgress;
    private int _totalDownload;

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

    public int CurrentProgress
    {
        get => _currentProgress;
        private set => _currentProgress = value;
    }

    public int TotalDownload
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
        if (step != GltfImportStep.Download) return;
        /*if (completed > 0)
            Debug.Log($"<color=red>{gameObject.name}</color> > {step}: {completed}/{total}");
        else
            Debug.Log($"{gameObject.name} > {step}: {completed}/{total}");*/
        CurrentProgress = completed;
        TotalDownload = total;
        OnDownloading?.Invoke();
    }

    private void Update()
    {
        _task?.MoveNext();
    }
}