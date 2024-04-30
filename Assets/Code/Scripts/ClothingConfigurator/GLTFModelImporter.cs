using UnityEngine;
using Piglet;
using UriUtil = Utility.UriUtil;
using System;

public class GLTFModelImporter : MonoBehaviour
{
    private GltfImportTask _task;
    private GameObject _model;

    public GameObject Model => _model;

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
    }

    private void OnProgress(GltfImportStep step, int completed, int total)
    {
        Debug.LogFormat("{0}: {1}/{2}", step, completed, total);
    }

    private void Update()
    {
        _task?.MoveNext();
    }
}
