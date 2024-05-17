using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlitedSliderPresenter : MonoBehaviour
{
    private Slider _progressSlider;
    [SerializeField] private float multiplaySlider;
    
    private void Awake()
    {
        _progressSlider = gameObject.GetComponent<Slider>();
        _progressSlider.value = 0f;
    }
    
    
    public void UpdateSlider2(int currentDownload,int totalDownload)
    {
        _progressSlider.maxValue = totalDownload;
        _progressSlider.value = currentDownload * multiplaySlider;
    }
}
