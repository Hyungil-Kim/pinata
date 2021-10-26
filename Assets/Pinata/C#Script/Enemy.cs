using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        target = gameManager.player;

    }

    // Update is called once per frame
    void Update()
	{
		switch (gameManager.CurrentState)
		{
			
			case GameManager.State.Turn:

				break;
			case GameManager.State.End:
				break;
			case GameManager.State.Finish:
				break;
		}

	}
}
