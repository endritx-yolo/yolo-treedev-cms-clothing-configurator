using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPresenter : MonoBehaviour
{
    private Slider _progressSlider;
    
    
    private void Awake()
    {
        _progressSlider = gameObject.GetComponent<Slider>();
        _progressSlider.value = 0f;
    }
    
    
    public void UpdateSlider(int currentDownload,int totalDownload)
    {
        _progressSlider.maxValue = totalDownload;
        _progressSlider.value = currentDownload;
    }
}
