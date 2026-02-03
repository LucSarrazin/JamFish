using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private InputSystem_Actions myActions;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float zoomSpeed = 1f;
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
    void Update()
    {
        Vector2 mousePos = GetMousePosition();
        Vector2 screenUV = new Vector2(mousePos.x / Screen.width - 0.5f, mousePos.y / Screen.height - 0.5f);
        
        Vector3 move = Vector3.zero;
        if (screenUV.x < -edge_threshold && transform.position.x > -6.5f) move.x = -1f;
        if (screenUV.x > edge_threshold && transform.position.x < 15.7f) move.x = 1f;
        if (screenUV.y < -edge_threshold && transform.position.y > -5.9f) move.y = -1f;
        if (screenUV.y > edge_threshold && transform.position.y < 6.35f) move.y = 1f;

        move = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * move.normalized;
        transform.Translate(Time.deltaTime * moveSpeed * move, Space.World);
    }

    private Vector2 GetMousePosition()
    {
        return myActions.Player.MousePos.ReadValue<Vector2>();
    }
}
