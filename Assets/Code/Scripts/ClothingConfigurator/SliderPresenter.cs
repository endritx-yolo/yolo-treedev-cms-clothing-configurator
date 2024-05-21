using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderPresenter : MonoBehaviour
{
    private Slider _progressSlider;
    [SerializeField] private float multiplySlider;
    
    private void Awake()
    {
        _progressSlider = gameObject.GetComponent<Slider>();
        _progressSlider.value = 0f;
    }
    
    
    public void UpdateSlider(float currentDownload,float totalDownload)
    {
        _progressSlider.maxValue = totalDownload;
        _progressSlider.value = currentDownload * multiplySlider;
    }
}
