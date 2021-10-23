using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    private GameManager gameManager;
	public float speed =5f;
	private float time = 0f;
	public bool finish = false;
	private Dreamteck.Splines.SplineFollower follower;
	private float saveSpeed;
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		
	}
	private void Start()
    {
		target = gameManager.player.transform;
		follower = target.gameObject.GetComponent<Dreamteck.Splines.SplineFollower>();
		
    }
    void LateUpdate()
    {
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Intro:
				IntroCamera();
				break;
			case GameManager.State.Start:
				saveSpeed = follower.followSpeed;
				followCamera();
				break;
			case GameManager.State.Turn:
				if (!finish)
				{
					zoomInCaremra();
				}
				else
				{
					
					if (transform.position.y > 9.5f)
					{
						time += Time.deltaTime;
						if (time > 5f)
						{
							changeState();
							followCamera();
						}
					}
					else
					{
						zoomOutCamera();
					}
				}
				if (follower.motion.rotationOffset.y >= 180 && !finish)
				{
					endingCamera();
				}
				break;
			case GameManager.State.End:
				followCamera();
				break;
			case GameManager.State.Finish:

				break;
			
		}

	}
	private void IntroCamera()
	{
		var cameraPos = target.position + target.right * 10;
		cameraPos.y += 10;
		transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(target);
	}
	private void followCamera()
	{
		var extraPos = new Vector3(target.position.x,target.position.y + 9,target.position.z);
		var cameraPos = target.position - target.forward * 15;
		cameraPos.y += 10;
		transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(extraPos);

	}
	private void zoomInCaremra()
	{
		var cameraPos = target.position - transform.forward * Mathf.Lerp(10f, 23f, 1f);
		cameraPos.y = 10;
		transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(target);
		
	}
	private void zoomOutCamera()
	{
		var cameraPos = target.position - transform.forward * Mathf.Lerp(23f, 10f, 1f);
		cameraPos.y = 10;
		transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(target);
		
	}
	private void endingCamera()
	{
		time += Time.deltaTime;
		var cameraPos = transform.up;
		cameraPos.y += 10;
		if(time < 1f)
		{
			transform.RotateAround(target.position, cameraPos, 180f*Time.deltaTime);
		}
		else
		{
			finish = true;
		}
	}
	private void changeState()
	{
		
		gameManager.setStateEnd();
	}
	
}
