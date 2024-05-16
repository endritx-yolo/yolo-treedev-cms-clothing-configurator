using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIImageColorTweener : MonoBehaviour, IReverseTweener, ISelectableTweener
{
    private Image _image;
    
    [SerializeField] private Color _newColor;
    [SerializeField] private float _duration = .25f;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private bool _isSpeedBased;
    [SerializeField] private Ease _ease = Ease.OutQuad;

    [BoxGroup("Selected")] [SerializeField] private Color _selectedColor;
    
    private Color _defaultColor;

    private Tween _tween;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
    }
    
    public void Execute()
    {
        KillTween();
        _tween = _image.DOColor(_newColor, _duration);
        InitializeTween();
    }

    public void ExecuteSelected()
    {
        KillTween();
        _tween = _image.DOColor(_selectedColor, _duration);
        InitializeTween();
    }

    public void Revert()
    {
        KillTween();
        _tween = _image.DOColor(_defaultColor, _duration);
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
