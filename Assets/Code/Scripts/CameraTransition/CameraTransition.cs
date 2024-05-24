using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using System;

public class CameraTransition : MonoBehaviour
{
    public static event Action OnAnyCameraTransitionFinished;

    [SerializeField] private float _startZPosition = -3f;
    [SerializeField] private float _zDestination = 0f;

    [BoxGroup("Tweening")] [SerializeField]
    private float _duration = 2f;

    [BoxGroup("Tweening")] [SerializeField]
    private Ease _ease = Ease.OutQuad;

    private Tween _moveTween;

    private void Awake()
    {
        Vector3 cachedPosition = transform.localPosition;
        cachedPosition.z = _startZPosition;
        transform.localPosition = cachedPosition;
    }

    private void OnEnable()
    {
        LoadSceneManager.OnAnyNewSceneLoaded += PlayCameraTransition;
    }

    private void OnDisable()
    {
        LoadSceneManager.OnAnyNewSceneLoaded -= PlayCameraTransition;
    }

    [Button("Test Transition")]
    private void PlayCameraTransition()
    {
        transform.DOLocalMoveZ(_zDestination, _duration)
            .SetEase(_ease)
            .OnComplete(() => OnAnyCameraTransitionFinished?.Invoke());
    }
}