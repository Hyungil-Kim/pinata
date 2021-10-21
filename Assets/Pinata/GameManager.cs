using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum State
    {   
        Intro,
        Start,
        Turn,
        End
    }
    public GameObject player;
    public GameObject[] obstacles;
    public GameObject[] enemys;
    public GameObject[] items;
    public GameObject camera2;
    public GameObject[] roads;
    public GameObject enemyParent;
    public Dreamteck.Splines.Spline spline;
    public double score;
    private State state;
    private Dreamteck.Splines.SplineFollower follower;
    private float saveSpeed;

    
	public State CurrentState
	{
		get { return state; }
		set
		{
			state = value;
			switch (state)
			{
				case State.Intro:
					break;
				case State.Start:
                    follower.followSpeed = saveSpeed;
                    break;
				case State.Turn:
					break;
				case State.End:
                    follower.followSpeed = saveSpeed;
                    follower.motion.rotationOffset = Vector3.zero;
                    follower.SetPercent(1d);
                    break;
			}
		}
	}

	// Start is called before the first frame update
	private void Awake()
	{
        CurrentState = State.Intro;
        player = GameObject.FindWithTag("Player");
        follower = player.GetComponent<Dreamteck.Splines.SplineFollower>();
        saveSpeed = follower.followSpeed;
    }
	void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        enemyParent = GameObject.FindWithTag("Respawn");
        enemyParent.SetActive(false);
        items = GameObject.FindGameObjectsWithTag("Item");
        roads = GameObject.FindGameObjectsWithTag("Road");
        camera2 = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setStateEnd()
    {
        CurrentState = State.End;
    }
    public void setStateTurn()
	{
        player.GetComponentInChildren<Animator>().SetTrigger("runTowalk");
        CurrentState = State.Turn;
    }
    public void setStateStart()
    {
        CurrentState = State.Start;
    }
    public void setStateIntro()
    {
        CurrentState = State.Intro;
    }
    public void setEnemyOn()
	{
        enemyParent.SetActive(true);
	}

}
