using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Common;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbitCameraLoadingScreen : SceneSingleton<OrbitCameraLoadingScreen>
{
    [FormerlySerializedAs("_leanPitchYaw")] [SerializeField] private LeanPitchYaw leanPitchYaw;

    private const float StartYaw = 0f;
    private const float EndYaw = 360f;
    private const float Duration = 6f;

    private Tweener _rotationTween;

    public void Awake()
    {
        _rotationTween = DOVirtual.Float(StartYaw, EndYaw, Duration, newValue => {
            leanPitchYaw.Yaw = newValue;
        });
    }

    public void StopRotation()
    {
        _rotationTween.Kill();
    }
    
 
}
