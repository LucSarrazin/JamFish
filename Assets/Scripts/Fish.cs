using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Fish : Interactable
{
    private SplineAnimate splineAnimate;
    private bool clicked = false;
    public float animationDuration = 3.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interacted fish");
        if (!clicked)
        {
            clicked = true;
            StartCoroutine(spline());
        }
    }

    IEnumerator spline()
    {
        splineAnimate.MaxSpeed = animationDuration;
        float savedElapsedTime = splineAnimate.ElapsedTime;
        float savedNormalizedTime = splineAnimate.NormalizedTime;
        splineAnimate.ElapsedTime = savedElapsedTime;
        splineAnimate.NormalizedTime = savedNormalizedTime;
        yield return new WaitForSeconds(3f);
        splineAnimate.MaxSpeed = 5f;
        savedElapsedTime = splineAnimate.ElapsedTime;
        savedNormalizedTime = splineAnimate.NormalizedTime;
        splineAnimate.ElapsedTime = savedElapsedTime;
        splineAnimate.NormalizedTime = savedNormalizedTime;
        clicked = false;
    }
}
