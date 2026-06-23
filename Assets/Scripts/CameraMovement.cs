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
    [SerializeField] private bool rightClick = false;
    [SerializeField] private float horizontalAxis;
    [SerializeField] private float verticalAxis;
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
        myActions.Player.MouseScroll.canceled += MouseScrollOncanceled;
        myActions.Player.RightClick.started += RightClickOnstarted;
        myActions.Player.RightClick.canceled += RightClickOncanceled;
        _cameraOrbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        _cameraController = GetComponent<CinemachineInputAxisController>();
        #if UNITY_WEBGL
                _cameraController.enabled = false;
        #else
            _cameraController.enabled = false;
        #endif

        targetRadius = _cameraOrbitalFollow.Radius;
        Cursor.lockState = CursorLockMode.None;
    }

    private void MouseScrollOncanceled(InputAction.CallbackContext obj)
    {
        _cameraController.enabled = false;
    }

    private void RightClickOnstarted(InputAction.CallbackContext obj)
    {
        // Debug.Log("Clique droit");
        // Debug.Log("Right Click Input : " + myActions.Player.RightClick.ReadValue<float>());
        // Debug.Log("Mouse Position : " + Mouse.current.position.ReadValue());
        Debug.Log("Value Horizontal Axis : " + _cameraOrbitalFollow.HorizontalAxis.Value);
        Debug.Log("Value Vertical Axis: " + _cameraOrbitalFollow.VerticalAxis.Value);
        #if UNITY_WEBGL
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
        #endif 
        _cameraController.enabled  = true;
        _cameraOrbitalFollow.HorizontalAxis.Value = horizontalAxis;
        _cameraOrbitalFollow.VerticalAxis.Value = verticalAxis;
        rightClick = true;
    }

    private void RightClickOncanceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("Clique droit annuler");
        #if UNITY_WEBGL
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        #endif
        _cameraController.enabled  = false;
        horizontalAxis = _cameraOrbitalFollow.HorizontalAxis.Value;
        verticalAxis = _cameraOrbitalFollow.VerticalAxis.Value;
        rightClick = false;
    }

    private void Update()
    {
        _cameraOrbitalFollow.Radius = Mathf.SmoothDamp(_cameraOrbitalFollow.Radius, targetRadius, ref zoomVelocity, 0.15f);

        #if UNITY_WEBGL
                    if (rightClick)
                    {
                        float mouseX = Input.GetAxis("Mouse X");
                        float mouseY = Input.GetAxis("Mouse Y");

                        _cameraOrbitalFollow.HorizontalAxis.Value += mouseX * cameraSpeed;
                        _cameraOrbitalFollow.VerticalAxis.Value -= mouseY * cameraSpeed;
                    }
                    
                    // scroll zoom
                    float scroll = Input.mouseScrollDelta.y;

                    if (scroll < 0 && _cameraOrbitalFollow.Radius < minZoom)
                    {
                        targetRadius += zoomSpeed * Time.deltaTime;
                    }
                    else if (scroll > 0 && _cameraOrbitalFollow.Radius > maxZoom)
                    {
                        targetRadius -= zoomSpeed * Time.deltaTime;
                    }
        #endif
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
            //Debug.Log("+Mouse Scroll");
            //Debug.Log("Scroll Input : "+ myActions.Player.MouseScroll.ReadValue<Vector2>());
            targetRadius += zoomSpeed * Time.deltaTime;
        }
        else if (context.ReadValue<Vector2>().y > 0 && _cameraOrbitalFollow.Radius > maxZoom)
        {
            //Debug.Log("-Mouse Scroll");
            //Debug.Log("Scroll Input : "+ myActions.Player.MouseScroll.ReadValue<Vector2>());
            targetRadius -= zoomSpeed * Time.deltaTime;
        }
    }
}
