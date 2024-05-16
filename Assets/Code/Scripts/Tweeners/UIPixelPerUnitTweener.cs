using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIPixelPerUnitTweener : MonoBehaviour, IReverseTweener, ISelectableTweener
{
    private Image _image;

    [SerializeField] private float _newPixelPerUnit;
    [SerializeField] private float _duration = .25f;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private bool _isSpeedBased;
    [SerializeField] private Ease _ease = Ease.OutQuad;

    [BoxGroup("Selected")] [SerializeField] private float _selectedPixelPerUnit;

    private float _defaultPixelPerUnit;

    private Tween _tween;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultPixelPerUnit = _image.pixelsPerUnitMultiplier;
    }

    public void Execute()
    {
        KillTween();
        _tween = DOVirtual.Float(_image.pixelsPerUnitMultiplier, _newPixelPerUnit, _duration,
            value => _image.pixelsPerUnitMultiplier = value);
        InitializeTween();
    }

    public void ExecuteSelected()
    {
        KillTween();
        _tween = DOVirtual.Float(_image.pixelsPerUnitMultiplier, _selectedPixelPerUnit, _duration,
            value => _image.pixelsPerUnitMultiplier = value);
        InitializeTween();
    }

    public void Revert()
    {
        KillTween();
        _tween = DOVirtual.Float(_image.pixelsPerUnitMultiplier, _defaultPixelPerUnit, _duration,
            value => _image.pixelsPerUnitMultiplier = value);
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