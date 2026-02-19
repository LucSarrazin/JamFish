using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    public virtual void OnHoverEnter() { }

    public virtual void OnHoverExit() { }
}

