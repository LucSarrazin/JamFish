using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Column : Interactable
{
    private Animator animator;
    private bool clicked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interacted column");
        if (!clicked)
        {
            clicked = true;
            animator.enabled = true;
        }
    }
}
