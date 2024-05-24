using DG.Tweening;
using UnityEngine;
using System;

public class ScreenFader : MonoBehaviour
{
    public static event Action OnAnyScreenFadeIn;
    public static event Action OnAnyScreenFadeOut;

    private CanvasGroup _canvasGroup;

    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private Ease _fadeEase = Ease.OutQuad;

    private Tween _fadeTween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        LoadingScreenCanvasManager.OnAnyLoadingFinished += FadeIn;
        LoadSceneManager.OnAnyNewSceneLoaded += FadeOut;
    }

    private void OnDisable()
    {
        LoadingScreenCanvasManager.OnAnyLoadingFinished -= FadeIn;
        LoadSceneManager.OnAnyNewSceneLoaded -= FadeOut;
    }

    private void FadeIn()
    {
        KillTween();
        _canvasGroup.blocksRaycasts = true;
        _fadeTween = _canvasGroup.DOFade(1f, _fadeDuration)
            .SetEase(_fadeEase)
            .OnComplete(() => OnAnyScreenFadeIn?.Invoke());
    }

    private void FadeOut()
    {
        KillTween();
        _canvasGroup.blocksRaycasts = false;
        _fadeTween = _canvasGroup.DOFade(0f, _fadeDuration)
            .SetEase(_fadeEase)
            .SetDelay(.5f)
            .OnComplete(() => OnAnyScreenFadeOut?.Invoke());
    }

    private void KillTween()
    {
        if (_fadeTween != null) _fadeTween.Kill();
    }
}