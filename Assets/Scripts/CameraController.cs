using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 20f;
    [SerializeField] private float rotationSpeed = 20f;

    [Header("Zoom")]
    [SerializeField, Tooltip("Максимальное приближение")]
    private float maxFieldOfView = 100;
    [SerializeField, Tooltip("Минимальное приближение")]
    private float minFieldOfView = 10f;
    [SerializeField] private float zoomSpeed = 10f;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float _targetFieldOfView = 50f;

    [SerializeField] private bool edgeScrollingEnabled;
    [SerializeField] private float edgeScrollingSize;
    
    private PlayerControls _playerControls;

    private void Start()
    {
        _playerControls = new PlayerControls();
        _playerControls.Camera.Enable();
    }

    private void Update()
    {
        Move();
        Rotate();
        Zoom();
    }

    private void Move()
    {
        var movementInput = _playerControls.Camera.Movement.ReadValue<Vector2>();
        var movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        
        if (edgeScrollingEnabled)
        {
            if (Input.mousePosition.x < edgeScrollingSize)
            {
                movementDirection.x -= 1f;
            }
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
            {
                movementDirection.x += 1f;
            }
            
            if (Input.mousePosition.y < edgeScrollingSize)
            {
                movementDirection.z -= 1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
            {
                movementDirection.z += 1f;
            }
        }
        
        
        
        var moveDistance = movingSpeed * Time.deltaTime;
        transform.position += movementDirection * moveDistance;
    }
    
    private void Rotate()
    {
        var rotationInput = _playerControls.Camera.Rotation.ReadValue<float>();
        var rotationDirection = new Vector3(0, rotationInput, 0);
        var rotationAngle = rotationSpeed * Time.deltaTime;

        transform.eulerAngles += rotationDirection * rotationAngle;
    }
    
    private void Zoom()
    {
        var inputScrollDelta = _playerControls.Camera.Zoom.ReadValue<Vector2>();
        if (inputScrollDelta.y == 0)
        {
            return;
        }

        _targetFieldOfView = inputScrollDelta.y > 0
            ? _targetFieldOfView - 5f
            : _targetFieldOfView + 5f;

        _targetFieldOfView = Mathf.Clamp(_targetFieldOfView, minFieldOfView, maxFieldOfView);

        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
            _targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void OnDisable()
    {
        _playerControls.Camera.Disable();
    }
}
