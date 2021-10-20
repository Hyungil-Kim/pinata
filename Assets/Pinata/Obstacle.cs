using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public double score;
    private Dreamteck.Splines.SplinePositioner splinePositioner;
    private Dreamteck.Splines.SplineComputer splineComputer;
    void Start()
    {
        splinePositioner = GetComponent<Dreamteck.Splines.SplinePositioner>();
        splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
        splinePositioner.spline = splineComputer;

    }
    void Update()
    {
        
    }
}
