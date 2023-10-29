using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraControls : MonoBehaviour
{
    [Inject] private PlayerControls _playerControls;

    private void Start()
    {
        _playerControls.Camera.Enable();
        
        _playerControls.Camera.Movement.performed += OnCameraMove;
        _playerControls.Camera.Rotation.performed += OnCameraRotate;
        _playerControls.Camera.Scale.performed += OnCameraScale;
    }

    private void OnCameraMove(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }
    
    private void OnCameraRotate(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }
    
    private void OnCameraScale(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        _playerControls.Camera.Movement.performed -= OnCameraMove;
        _playerControls.Camera.Rotation.performed -= OnCameraRotate;
        _playerControls.Camera.Scale.performed -= OnCameraScale;
        
        _playerControls.Camera.Disable();
    }
}
