using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public enum State
	{
		Intro,
		Start,
		Turn,
		End,
		Finish,
		dead
	}
	[HideInInspector]
	public GameObject player;
	[HideInInspector]
	public GameObject[] obstacles;
	[HideInInspector]
	public GameObject[] enemys;
	[HideInInspector]
	public GameObject[] items;
	[HideInInspector]
	public GameObject camera2;
	[HideInInspector]
	public GameObject enemyParent;
	[HideInInspector]
	public Dreamteck.Splines.Spline spline;
	[HideInInspector]
	public double score;
	[HideInInspector]
	public Dreamteck.Splines.SplineFollower follower;
	[HideInInspector]
	private State state;
	[HideInInspector]
	private float saveSpeed;
	[HideInInspector]
	public double totalScore = 0f;
	[HideInInspector]
	private Scene scene;
	[HideInInspector]
	public int stageLevel;

	public UIManager UiController;
	[HideInInspector]
	public Button playButton;
	[HideInInspector]
	public Text levelText;
	[HideInInspector]
	public bool gameStart;
	[HideInInspector]
	public double percentScore;
	[HideInInspector]
	public int savegold;
	[HideInInspector]
	public int earnGold;
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
					UiController.ingameScene.SetActive(true);
					break;
				case State.Turn:
					score = player.GetComponent<Player>().score;
					break;
				case State.End:
					score = player.GetComponent<Player>().score;
					follower.followSpeed = saveSpeed;
					follower.motion.rotationOffset = Vector3.zero;
					follower.SetPercent(1d);
					break;
				case State.Finish:
					percentScore = score / totalScore;
					savegold += earnGold;
					break;
				case State.dead:
					follower.followSpeed = 0;
					player.GetComponentInChildren<Animator>().speed = 0;
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
		camera2 = GameObject.FindWithTag("MainCamera");
		foreach (var item in items)
		{
			totalScore += item.GetComponent<Item>().score;
		}
		stageLevel = scene.buildIndex + 1;
	}

	// Update is called once per frame
	void Update()
	{
		switch (state)
		{
			case State.Intro:
				break;
			case State.Start:
			case State.Turn:
			case State.End:
			case State.Finish:
				earnGold = player.GetComponent<Player>().gold;
				break;
			case State.dead:
				break;
		}
	}
	public void setStateEnd()
	{
		player.GetComponentInChildren<Animator>().SetTrigger("castTorun");
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
	}
	public void setStateIntro()
	{
		CurrentState = State.Intro;
	}
	public void setStatedead()
	{
		CurrentState = State.dead;
	}
	public void setEnemyOn()
	{
		enemyParent.SetActive(true);
	}

	public string changeUnit(double score)
	{
		if(score/1000 >= 1)//k
		{
			score /= 1000;
			if (score / 1000 >= 1)//m
			{
				score /= 1000;
				return $"{ score.ToString("F1")}M";
			}
			return $"{ score.ToString("F1")}K";
			
		}
		else
		{
			return score.ToString();
		}
	}


}
