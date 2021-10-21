using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button playButton;
    public Text levelText;
    public Slider showGoal;
    public int stageLevel = 1;
    public bool gameStart;
	public Button Restart;
	public Button Option;
    private GameManager gameManager;
    
	// Start is called before the first frame update
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	void Start()
    {
       // levelText.text = $"Stage {stageLevel}";
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
		}

	}
	public void setTriggerTrue()
    {
		gameStart = true;
		playButton.gameObject.SetActive(false);
		levelText.gameObject.SetActive(false);
		levelText.text = $"Stage {stageLevel}";
	}
}
