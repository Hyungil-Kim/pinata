using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public double score;
    private GameManager gameManager;
    private Dreamteck.Splines.SplinePositioner splinePositioner;
    private Dreamteck.Splines.SplineComputer splineComputer;
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	void Start()
    {
        splinePositioner = GetComponent<Dreamteck.Splines.SplinePositioner>();
        splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
        splinePositioner.spline = splineComputer;
    }
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position,gameManager.player.transform.position) < 10f)
		{
            GetComponentInChildren<Animator>().SetTrigger("idleToattack");
        }
		else
		{
            GetComponentInChildren<Animator>().SetTrigger("attackToidle");
        }
    }
}
