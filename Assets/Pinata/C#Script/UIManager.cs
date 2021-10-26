using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button playButton;
    public Text levelText;
    public Slider showGoal;
	public Button restart;
	public Button optionButton;
	public Button endButton;
	public Text endScore;
	public Text endGrade;
	public GameObject optionPanel;
	public GameObject endManager;
    private GameManager gameManager;
	public OptionManager optionManager;
    public int stageLevel = 1;
    public bool gameStart;
	public bool pause;

	private float time;
	// Start is called before the first frame update
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	void Start()
    {
       levelText.text = $"Stage {stageLevel}";
    }

    // Update is called once per frame
    void Update()
	{
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Intro:
				break;
			case GameManager.State.Start:

				showGoal.value = (float)gameManager.follower.GetPercent();
				break;
			case GameManager.State.Turn:
			case GameManager.State.End:
				break;
			case GameManager.State.Finish:
				time += Time.deltaTime;
				if (time > 5f)
				{
					endManager.SetActive(true);
				}
				endScore.text = $"You Earned {gameManager.score}";
				if (gameManager.score != 0)
				{
					if (gameManager.percentScore == 1)
					{
						endGrade.text = "PerPect";
					}
					else if (gameManager.percentScore >= 0.7)
					{
						endGrade.text = "Awesome";
					}
					else if (gameManager.percentScore >= 0.4)
					{
						endGrade.text = "Good";
					}
					else
					{
						endGrade.text = "Poor";
					}

					if(gameManager.percentScore >= 0.4)
					{
						endButton.GetComponentInChildren<Text>().text = "Next Stage";
					}
					else
					{
						endButton.GetComponentInChildren<Text>().text = "Retry";
					}
				}
				else
				{
					endGrade.text = "Poor";
					endButton.GetComponentInChildren<Text>().text = "Retry";

				}
				break;
		}
		if(pause)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
	public void setTriggerTrue()
    {
		gameStart = true;
		playButton.gameObject.SetActive(false);
		levelText.gameObject.SetActive(false);
		levelText.text = $"Stage {stageLevel}";
	}
	public void OnclickRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void OnclickOption()
	{
		if (!pause)
		{
			optionPanel.SetActive(true);
			pause = true;
		}

	}
	public void OnclickEndButton()
	{
		if (gameManager.score != 0)
		{
	     	if (gameManager.percentScore >= 0.4)
			{
				//nextstage
			}
			else
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

}
