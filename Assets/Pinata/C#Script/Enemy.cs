using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject target;
	private Animator animator;
	private float time;
	public float speed;
	public float minSpeed;
	public float maxSpeed;
	private Dreamteck.Splines.SplineFollower splineFollower;
	private Dreamteck.Splines.SplineComputer splineComputer;
	private double enemyPersent;

	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		animator = GetComponent<Animator>();
		splineFollower = GetComponent<Dreamteck.Splines.SplineFollower>();
		enemyPersent = GetComponentInParent<EnemyLine>().persent;
	}
	void Start()
	{
        target = gameManager.player;
		splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
		splineFollower.spline = splineComputer;
		splineFollower.SetPercent(enemyPersent);
	}
    // Update is called once per frame
    void Update()
	{
		speed = Random.Range(minSpeed, maxSpeed);
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Intro:
			case GameManager.State.Start:
			case GameManager.State.dead:
				splineFollower.followSpeed = 0;
				break;
			case GameManager.State.Turn:
				time += Time.deltaTime;
				if (time > 2f)
				{
					animator.SetTrigger("idleToturn");
					splineFollower.followSpeed = 0f;
				}
				break;
			case GameManager.State.End:
				splineFollower.followSpeed = speed;
				break;
			case GameManager.State.Finish:
				splineFollower.followSpeed = 0f;
				break;
		}
	}
}
