using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SliderPresenter : MonoBehaviour
{
    [SerializeField] private Slider _greenSlider;
    [SerializeField] private Slider _whiteSlider;

    [BoxGroup("Tweening")] [SerializeField]
    private float _followSpeed = 1f;

    private Tween _sliderTween;

    private void Awake()
    {
        _greenSlider.value = 0f;
        _whiteSlider.value = 0f;
    }

    public void UpdateSlider(float currentDownload, float totalDownload)
    {
        _greenSlider.maxValue = totalDownload;
        _greenSlider.value = currentDownload;

        UpdateWhiteSlider();
    }

    private void UpdateWhiteSlider()
    {
        KillTween();
        _sliderTween = _whiteSlider.DOValue(_greenSlider.value, _followSpeed).SetSpeedBased()
            .SetEase(Ease.OutQuad);
    }

    private void KillTween()
    {
        if (_sliderTween != null) _sliderTween.Kill();
    }
}