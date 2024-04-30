using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
    [BoxGroup("Tweening"), SerializeField] private float _duration = .5f;
    [BoxGroup("Tweening"), SerializeField] private float _delay = 1f;
    [BoxGroup("Tweening"), SerializeField] private Ease _fadeEase = Ease.OutSine;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        TweenCanvasGroup();
    }

    private void TweenCanvasGroup()
    {
        _canvasGroup.DOFade(0f, _duration).SetDelay(_delay).SetEase(_fadeEase).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}