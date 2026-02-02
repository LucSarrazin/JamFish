using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    private InputSystem_Actions myActions;
    [SerializeField] private float moveSpeed = 10f;
    private const float edge_threshold = 0.45f;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = GetMousePosition();
        Vector2 screenUV = new Vector2(mousePos.x / Screen.width - 0.5f, mousePos.y / Screen.height - 0.5f);
        
        Vector3 move = Vector3.zero;
        if (screenUV.x < -edge_threshold) move.x = -1f;
        if (screenUV.x > edge_threshold) move.x = 1f;
        if (screenUV.y < -edge_threshold) move.y = -1f;
        if (screenUV.y > edge_threshold) move.y = 1f;

        move = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * move.normalized;
        transform.Translate(Time.deltaTime * moveSpeed * move, Space.World);
    }

    private Vector2 GetMousePosition()
    {
        return myActions.Player.Mouse.ReadValue<Vector2>();
    }
}
