using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    private InputSystem_Actions myActions;
    [SerializeField] private float zoomSpeed = 5f;
    private CinemachineSplineDolly _camera;

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
        _camera = GetComponent<CinemachineSplineDolly>();
    }

    private void MouseScroll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().y > 0)
        {
            _camera.CameraPosition += zoomSpeed * Time.deltaTime;
        }
        else
        {
            _camera.CameraPosition -= zoomSpeed * Time.deltaTime;
        }
    }
}
