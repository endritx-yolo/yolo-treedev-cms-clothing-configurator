using DG.Tweening;
using UnityEngine;

public class UISizeDeltaTweener : MonoBehaviour, IReverseTweener
{
    private RectTransform _rectTransform;
    
    [SerializeField] private Vector2 _newSizeDelta;
    [SerializeField] private float _duration = .25f;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private bool _isSpeedBased;
    [SerializeField] private Ease _ease = Ease.OutQuad;
    
    private Vector2 _defaultSizeDelta;

    private Tween _tween;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultSizeDelta = _rectTransform.sizeDelta;
    }
    
    public void Execute()
    {
        KillTween();
        _tween = _rectTransform.DOSizeDelta(_newSizeDelta, _duration);
        InitializeTween();
    }

    public void Revert()
    {
        KillTween();
        _tween = _rectTransform.DOSizeDelta(_defaultSizeDelta, _duration);
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
