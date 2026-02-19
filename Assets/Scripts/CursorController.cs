using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorClickedTexture;

    private Interactable currentInteractable;

    private void Awake()
    {
        ChangeCursor(cursorTexture);
        Cursor.lockState = CursorLockMode.Confined;
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

        if (Physics.Raycast(ray, out hit))
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
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }
}