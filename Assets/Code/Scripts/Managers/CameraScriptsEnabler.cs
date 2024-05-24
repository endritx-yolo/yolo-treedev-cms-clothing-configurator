using Lean.Touch;
using UnityEngine;

public class CameraScriptsEnabler : MonoBehaviour
{
    [SerializeField] private LeanTouch _leanTouch;

    private void Awake()
    {
        _leanTouch = FindObjectOfType<LeanTouch>();
    }

    private void OnEnable()
    {
        CameraTransition.OnAnyCameraTransitionFinished += EnableScripts;
    }

    private void OnDisable()
    {
        CameraTransition.OnAnyCameraTransitionFinished -= EnableScripts;
    }
    
    private void EnableScripts()
    {
        _leanTouch.UseTouch = true;
        _leanTouch.UseHover = true;
        _leanTouch.UseMouse = true;
        _leanTouch.UseSimulator = true;
    }
}
