using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCube : MonoBehaviour
{
    private InputSystem_Actions myActions;
    private Vector2 mousePos;
    [SerializeField] private float force;
    [SerializeField] private GameObject cube;
    private bool isDragging = false;
    private bool _inverted;
    private void OnEnable()
    {
        myActions = new InputSystem_Actions();
        myActions.Player.Enable();
    }

    private void OnDisable()
    {
        myActions.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myActions.Player.MoveCube.started += dragging;
        myActions.Player.MoveCube.canceled += notDragging;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = GetMousePosition();
        Vector2 screenUV = new Vector2(mousePos.x / Screen.width - 0.5f, mousePos.y / Screen.height - 0.5f);

        if (isDragging)
        {
            float rotX = screenUV.x * force* Time.deltaTime;
            //transform.Rotate(Vector3.up * (_inverted ? 1 : -1), rotX, Space.World);
            transform.RotateAround(cube.transform.position, Vector3.up, rotX);
        }
    }

    private Vector2 GetMousePosition()
    {
        return myActions.Player.MousePos.ReadValue<Vector2>();
    }

    void dragging(InputAction.CallbackContext context)
    {
        Debug.unityLogger.Log("dragging");
        isDragging = true;
    }

    void notDragging(InputAction.CallbackContext context)
    {
        Debug.unityLogger.Log("notDragging");
        isDragging = false;
    }

}
