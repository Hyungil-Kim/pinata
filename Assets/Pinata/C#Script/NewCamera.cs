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
	private float rottime = 0f;
	private float lateTime = 0f;
	private float gameOverTime = 0f;
	
	private Vector3 curpos;
	private float changetime =0f;
	public bool finish = false;
	public bool anistart = false;
	private Dreamteck.Splines.SplineFollower follower;
	private float saveSpeed;

	private bool change;
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		
	}
	private void Start()
    {
		target = gameManager.player.transform;
		follower = target.gameObject.GetComponent<Dreamteck.Splines.SplineFollower>();
		curpos = transform.position;
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
				if (!finish)//전환중
				{
					lateTime += Time.deltaTime;
					if (lateTime > 1f)
					{

					}
				}
				else
				{
					
					if (transform.position.y > 9.5f)
					{
						time += Time.deltaTime;
						if (time > 1f)
						{
							gameManager.setStateEnd();
							
						}
						else
						{
						}
					}
				}
				if (follower.motion.rotationOffset.y >= 180 && !finish)//완전히 전환
				{
					if (!anistart)
					{
						gameManager.player.GetComponentInChildren<Animator>().SetTrigger("walkTocast");
						anistart = true;
					}
					endingCamera();
				}
				break;
			case GameManager.State.End:
				followCamera();
				break;
			case GameManager.State.Finish:
				//엔딩씬 연출
				break;
			case GameManager.State.dead:
				deadCamera();
				gameOverTime += Time.deltaTime;
				if(gameOverTime >3f)
				{
					gameManager.UiController.gameOverScene.gameObject.SetActive(true);
				}
				break;
		}

	}
	private void IntroCamera()
	{
		if (gameManager.UiController.gameStart)
		{
			changetime += Time.deltaTime;
			var extraPos = new Vector3(target.position.x, target.position.y + 9, target.position.z);
			var cameraPos = target.position - target.forward * 15 + target.up * 10;
			transform.position = Vector3.Lerp(curpos, cameraPos, changetime / 1f);
			transform.LookAt(target);
		}
	}

	private void followCamera()
	{
		
		var extraPos = new Vector3(target.position.x,target.position.y +4,target.position.z);
		var cameraPos = target.position - target.forward * 15* target.localScale.x/2 + target.up *17*target.localScale.x/2;
		transform.position = cameraPos;
		transform.LookAt(extraPos);
		

	}
	private void endingCamera()
	{
		rottime += Time.deltaTime;
		var cameraPos = transform.up;
		cameraPos.y += 10;
		if(rottime < 1f)
		{
			transform.RotateAround(target.position, Vector3.up, 180f*Time.deltaTime);
		}
		else
		{
			finish = true;
		}
	}
	private void deadCamera()
	{
		//var cameraPos = target.position + target.right * 10;
		//cameraPos.y += 10;
		//transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(target);
	}
}
