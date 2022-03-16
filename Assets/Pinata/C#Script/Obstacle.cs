using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public double score;
    public float scale;
    public int candy;
    public Dreamteck.Splines.SplinePositioner splinePositioner;
    public Dreamteck.Splines.SplineComputer splineComputer;
    void Start()
    {
        splinePositioner = GetComponent<Dreamteck.Splines.SplinePositioner>();
        splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
        splinePositioner.spline = splineComputer;
        GetComponentInChildren<Animator>().SetTrigger("idleToattack");

    }
}
