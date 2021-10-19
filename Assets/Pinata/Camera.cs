using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    private GameManager gameManager;
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		
	}
	private void Start()
    {
		target = gameManager.player.transform;
    }
    void Update()
    {
        
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Start:
				
					var camerPos = target.transform.position;
					camerPos.y += 10;
					camerPos.z -= 23;
					//Debug.Log(transform.position);
					transform.position = camerPos;
					transform.LookAt(target); ;

			
				break;
			case GameManager.State.End:
				
					camerPos = target.transform.position;
					camerPos.y += 10;
					camerPos.z += 23;
					//Debug.Log(transform.position);
					transform.position = camerPos;
					transform.LookAt(target); ;
								
				break;
		}
		

    }
}
