using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
   [SerializeField] private float zoomSpeed = 10f;
   [SerializeField] private float smoothness = 5.0f;
   private const float MinFOV = 33f;
   private const float MaxFOV = 50f;

    private Camera _cam;
    private float _targetFOV;
    private float _initialFOV;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _initialFOV = _cam.fieldOfView;
        _targetFOV = _initialFOV;
    }

    private void Update()
    {
        var scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            _targetFOV -= scrollInput * zoomSpeed;
            _targetFOV = Mathf.Clamp(_targetFOV, MinFOV, MaxFOV); 
        }
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _targetFOV, Time.deltaTime * smoothness);
    }

   
}
