using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Crab : Interactable
{
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private SplineAnimate splineAnimateFlee;
    private bool clicked = false;
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
        Debug.Log("Interacted crab");
        if (!clicked)
        {
            clicked = true;
            splineAnimate.enabled = false;
            splineAnimateFlee.enabled = true;
            // StartCoroutine(spline());
        }
    } 
    // IEnumerator spline()
    // {
    //     splineAnimate.enabled = false;
    //     splineAnimateFlee.enabled = true;
    //     yield return new WaitForSeconds(10f);
    //     splineAnimateFlee.enabled = false;
    //     splineAnimate.enabled = true;
    //     clicked = false;
    // }
}
