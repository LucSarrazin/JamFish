using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    private InputSystem_Actions myActions;
    private CinemachineOrbitalFollow _cameraOrbitalFollow;
    private CinemachineInputAxisController _cameraController;
    
    [Header("Camera Settings")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomSmooth = 8f;
    [SerializeField] private float maxZoom;
    [SerializeField] private float minZoom;
    [SerializeField] private float cameraSpeed;
    public float CameraSpeed
    {
        get { return cameraSpeed; }
        set
        {
            cameraSpeed = value;
            changeSpeed();
        }
    }
    private float targetRadius;
    private float zoomVelocity;


    private void OnEnable()
    {
        myActions = new InputSystem_Actions();
        myActions.Player.Enable();
    }

    private void OnDisable()
    {
        myActions.Player.Disable();
    }

    void Start()
    {
        myActions.Player.MouseScroll.performed += MouseScroll;
        _cameraOrbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        _cameraController = GetComponent<CinemachineInputAxisController>();

        targetRadius = _cameraOrbitalFollow.Radius;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        _cameraOrbitalFollow.Radius = Mathf.SmoothDamp(
            _cameraOrbitalFollow.Radius,
            targetRadius,
            ref zoomVelocity,
            0.15f
        );
    }

    private void changeSpeed()
    {
        foreach (var c in _cameraController.Controllers)
        {
            if (c.Name == "Look Orbit X")
            {
                c.Input.Gain = cameraSpeed;
                break;
            }
        }
        foreach (var c in _cameraController.Controllers)
        {
            if (c.Name == "Look Orbit Y")
            {
                c.Input.Gain = -cameraSpeed;
                break;
            }
        }
    }

    private void MouseScroll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().y < 0 && _cameraOrbitalFollow.Radius < minZoom)
        {
            targetRadius += zoomSpeed * Time.deltaTime;
        }
        else if (context.ReadValue<Vector2>().y > 0 && _cameraOrbitalFollow.Radius > maxZoom)
        {
            targetRadius -= zoomSpeed * Time.deltaTime;
        }
    }
}
