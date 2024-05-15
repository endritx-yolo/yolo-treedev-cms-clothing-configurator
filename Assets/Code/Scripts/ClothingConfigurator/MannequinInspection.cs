using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MannequinInspection : MonoBehaviour
{
    private bool _interactable;
    private bool _interacted;
    private CameraInspectorEffect _cameraInspectorEffect;
    private bool _isExamining;
    private Camera _camera;
    private PlayerInput _playerInput;
    private Vector3 _lastMousePosition;
    private MeshRenderer _meshRenderer;
    private Transform _examinedObject;
    private Quaternion _startRotation;
    private float _currentRotation;

    private void Start()
    {
        Transform transform1;
        _meshRenderer = (transform1 = transform).GetChild(0).GetComponent<MeshRenderer>();
        _startRotation = transform1.rotation;
    }

    private void Awake()
    {
        _cameraInspectorEffect = FindObjectOfType<CameraInspectorEffect>();
        _camera = Camera.main;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController player))
        {
            _playerInput = player.GetComponent<PlayerInput>();
        }
        _interactable = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out ThirdPersonController player)) return;
        _interactable = false;
        _cameraInspectorEffect.ResetCameraSettings();
    }
    private void Update()
    {
        if (_isExamining)
        {
            Examine();
        }
        if (!Input.GetKeyDown(KeyCode.E)) return;
        _isExamining = !_isExamining;
        if (!_interactable) return;
        if (_isExamining)
        {
            _cameraInspectorEffect.InspectionCameraEffect();
            var offset = new Vector3(3f, 2f, 0f);
            var offset2 = new Vector3(-3f, 2f, 0f);
            if (transform.position.x < 0)
                _camera.transform.position = transform.position +offset;
            else
                _camera.transform.position = transform.position +offset2;
            
            _camera.transform.LookAt(_meshRenderer.bounds.center);
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Object"))
                {
                    if (_isExamining)
                    {
                        _examinedObject = hit.transform;

                    }
                }
            }
            StartExamination();
           
        }
        else
        {
            _cameraInspectorEffect.ResetCameraSettings();
            StopExamination();
        }
    }

    protected virtual void StartExamination()
    {
        _lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerInput.enabled = false;
    }

    protected virtual void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput.enabled = true;
    }

    private void Examine()
    {
        if (_examinedObject == null) return;
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = Input.mousePosition.x - _lastMousePosition.x;
            const float speedRotation = .8f;
            Quaternion rotate = Quaternion.Euler(0, (-deltaMouse * speedRotation) + _currentRotation, 0);
            transform.rotation = rotate;
        }
        else
        {
            _currentRotation = transform.eulerAngles.y;
            _lastMousePosition = Input.mousePosition;
        }


    }
}

