using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum State
    {
        Start,
        End
    }
    public GameObject player;
    public GameObject[] obstacles;
    public GameObject[] enemys;
    public GameObject[] items;
    public GameObject camera2;
    public GameObject[] roads;
    public Dreamteck.Splines.Spline spline;
    public int score;
    private State state;

    
	public State CurrentState
	{
		get { return state; }
		set
		{
			state = value;
			
		}
	}

	// Start is called before the first frame update
	private void Awake()
	{
        CurrentState = State.Start;
    }
	void Start()
    {
        player = GameObject.FindWithTag("Player");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        items  = GameObject.FindGameObjectsWithTag("Item");
        roads = GameObject.FindGameObjectsWithTag("Road");
        camera2 = GameObject.FindWithTag("MainCamera");
        spline = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.Spline>();
    }

    // Update is called once per frame
    void Update()
    {
		
	}
    public void setStateEnd()
    {
        CurrentState = State.End;
    }
    public void setStateStart()
    {
        CurrentState = State.Start;
    }
}
