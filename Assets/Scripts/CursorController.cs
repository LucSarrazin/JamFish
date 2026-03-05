using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorClickedTexture;

    [SerializeField] private LayerMask interactableLayer;
    private Interactable currentInteractable;

    private void Awake()
    {
        ChangeCursor(cursorTexture);
        Cursor.lockState = CursorLockMode.None;
    }


    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            ChangeCursor(cursorClickedTexture);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                ChangeCursor(cursorClickedTexture);

                if (currentInteractable != interactable)
                {
                    currentInteractable?.OnHoverExit();
                    currentInteractable = interactable;
                    currentInteractable.OnHoverEnter();
                }

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    interactable.Interact();
                }

                return;
            }
        }

        currentInteractable?.OnHoverExit();
        currentInteractable = null;
        ChangeCursor(cursorTexture);
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 1, cursorType.height / 1);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }
}