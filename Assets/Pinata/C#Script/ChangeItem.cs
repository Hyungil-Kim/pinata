using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItem : MonoBehaviour
{
    private Dreamteck.Splines.SplinePositioner splinePositioner;
    private Dreamteck.Splines.SplineComputer splineComputer;
    // Start is called before the first frame update
    void Start()
    {
        splinePositioner = GetComponent<Dreamteck.Splines.SplinePositioner>();
        splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
        splinePositioner.spline = splineComputer;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
