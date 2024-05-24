using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System;

public class LoadSceneManager : MonoBehaviour
{
    public static event Action OnAnyNewSceneLoaded;

    [Scene] [SerializeField] private string sceneToLoadName;

    private void OnEnable()
    {
        ScreenFader.OnAnyScreenFadeIn += TryLoadNextScene;
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }

    private void OnDisable()
    {
        ScreenFader.OnAnyScreenFadeIn -= TryLoadNextScene;
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }

    private void TryLoadNextScene() => SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);

    private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode) => OnAnyNewSceneLoaded?.Invoke();
}