using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class RotateHorizontalTweener : MonoBehaviour, ITweener
{
    [SerializeField] private Transform _objectToRotateTransform;
    [HideIf("_isSpeedBased"), SerializeField] private float _rotateDuration = 2f;
    [ShowIf("_isSpeedBased"), SerializeField] private float _rotateSpeed = 2f;
    [SerializeField] private Ease _rotateEase = Ease.Linear;
    [SerializeField] private bool _isSpeedBased;

    private Tween _tween;
    private Vector3 _rotation = new Vector3(0f, 180f, 0f);

    private void Start() => Execute();

    public void Execute()
    {
        float value = _isSpeedBased? _rotateSpeed : _rotateDuration;
        _tween = _objectToRotateTransform.DOLocalRotate(_rotation, value)
            .SetEase(_rotateEase)
            .SetLoops(-1, LoopType.Incremental);

        if (_isSpeedBased) _tween.SetSpeedBased();
    }
}