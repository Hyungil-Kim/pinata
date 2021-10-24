using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public enum State
    {   
        Intro,
        Start,
        Turn,
        End,
        Finish
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
    public Dreamteck.Splines.SplineFollower follower;
    private State state;
    private float saveSpeed;
    public double totalScore = 0f;

    public UIManager UiController;
    public int stageLevel =1;
    public Button playButton;
    public Text levelText;
    public bool gameStart;
    public double percentScore;
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
					UiController.showGoal.gameObject.SetActive(true);
					//UiController.restart.gameObject.SetActive(true);
					//UiController.option.gameObject.SetActive(true);
					break;
				case State.Turn:
					break;
				case State.End:
					follower.followSpeed = saveSpeed;
					follower.motion.rotationOffset = Vector3.zero;
					follower.SetPercent(1d);
					break;
				case State.Finish:
                    percentScore = score / totalScore;
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
        UiController = GameObject.FindWithTag("UIController").GetComponent<UIManager>();
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
        foreach(var item in items)
		{
            totalScore += item.GetComponent<Item>().score;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void setStateEnd()
    {
        player.GetComponentInChildren<Animator>().SetTrigger("walkTorun");
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
    public void setStateFinish()
	{
        CurrentState = State.Finish;
        player.GetComponentInChildren<Animator>().SetTrigger("runTo");
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
