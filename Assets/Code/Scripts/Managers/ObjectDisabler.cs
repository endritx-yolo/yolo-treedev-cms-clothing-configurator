using System;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjectsToDisable;

    private void OnEnable()
    {
        ScreenFader.OnAnyScreenFadeIn += DisableGameObjects;
    }

    private void OnDisable()
    {
        ScreenFader.OnAnyScreenFadeIn -= DisableGameObjects;
    }

    private void DisableGameObjects()
    {
        for (int i = 0; i < _gameObjectsToDisable.Length; i++)
            _gameObjectsToDisable[i].SetActive(false);
    }
}