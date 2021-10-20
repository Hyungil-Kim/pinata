using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum State
    {
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
        player = GameObject.FindWithTag("Player");
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
        CurrentState = State.Turn;
    }
    public void setStateStart()
    {
        CurrentState = State.Start;
    }
    public void setEnemyOn()
	{
        enemyParent.SetActive(true);
	}
}
