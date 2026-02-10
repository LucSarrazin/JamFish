using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private InputSystem_Actions myActions;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float zoomSpeed = 1f;
    private const float edge_threshold = 0.45f;
    
    
    [Header("World Bounds")]
    [SerializeField] private float minX = -6.5f;
    [SerializeField] private float maxX = 15.7f;
    [SerializeField] private float minY = -5.9f;
    [SerializeField] private float maxY = 6.35f;
    
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

        if (screenUV.x < -edge_threshold) move.x = -1f;
        if (screenUV.x >  edge_threshold) move.x =  1f;
        if (screenUV.y < -edge_threshold) move.y = -1f;
        if (screenUV.y >  edge_threshold) move.y =  1f;

        if (move.sqrMagnitude > 0f)
        {
            move = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * move.normalized;
            transform.position += move * moveSpeed * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;
        }
    }

    private Vector2 GetMousePosition()
    {
        return myActions.Player.MousePos.ReadValue<Vector2>();
    }
}
