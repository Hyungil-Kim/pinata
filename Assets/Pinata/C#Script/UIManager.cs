using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
	public OptionManager optionManager;
	public GameObject startScene;
	public GameObject ingameScene;
	public GameOver gameOverScene;
	public GameObject optionPanel;
	public EndUiScript endScene;
	public GameObject animationCamera;
	public ShopScript shopPanel;

    public Button playButton;
    public Text levelText;
    public Slider showGoal;
	public Slider tutorial;
	public Button restart;
	public Button optionButton;
	public Button shopButton;
	public Button adButton;
	public Text cake;
    private int stageLevel;
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
		stageLevel = gameManager.stageLevel;
       levelText.text = $"STAGE {stageLevel}";
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
				cake.text = gameManager.changeUnit(gameManager.earnGold);
				break;
			case GameManager.State.Turn:
			case GameManager.State.End:
				break;
			case GameManager.State.Finish:
				time += Time.deltaTime;
				if (time > 2f && endScene.gameObject.activeSelf == false)
				{
					ingameScene.SetActive(false);
					optionButton.gameObject.SetActive(false);
					endScene.gameObject.SetActive(true);
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
		startScene.SetActive(false);
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
	public void OnclickAdButton()
	{
		//±§∞ÌΩ√√ª
		gameManager.Save();
	}
	public void OnclickShopButton()
	{
		shopPanel.gameObject.SetActive(true);
		startScene.SetActive(false);
		optionButton.gameObject.SetActive(false);
	}
}
