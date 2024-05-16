using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIFontSizeTweener : MonoBehaviour, IReverseTweener
{
    private TextMeshProUGUI _text;

    [SerializeField] private float _newFonstSize;
    [SerializeField] private float _duration = .25f;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private bool _isSpeedBased;
    [SerializeField] private Ease _ease = Ease.OutQuad;

    private float _defaultFontSize;

    private Tween _tween;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _defaultFontSize = _text.fontSize;
    }

    public void Execute()
    {
        KillTween();
        _tween = _text.DOFontSize(_newFonstSize, _duration);
        InitializeTween();
    }

    public void Revert()
    {
        KillTween();
        _tween = _text.DOFontSize(_defaultFontSize, _duration);
        InitializeTween();
    }

    private void InitializeTween()
    {
        _tween.SetEase(_ease);
        _tween.SetDelay(_delay);
        _tween.SetSpeedBased(_isSpeedBased);
    }

    public void KillTween()
    {
        if (_tween == null) return;
        _tween.Kill();
    }
}