using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private Animator animator;
    private bool open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interacted Chest");
        if (!open)
        {
            open = true;
            animator.SetBool("Open", true);
            animator.SetBool("Close", false);
        }
        else
        {
            open = false;
            animator.SetBool("Open", false);
            animator.SetBool("Close", true);
        }
    }
}
