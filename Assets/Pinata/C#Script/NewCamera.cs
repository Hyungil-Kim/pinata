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
				lateTime += Time.deltaTime;
				
				if (!finish)//전환중
				{
					if (lateTime > 1f)
					{
						followCamera();

					}
			
				}
				else
				{

					if (transform.position.y > 9.5f)
					time += Time.deltaTime;
					if (time > 2f)
					{
						followCamera();
						//backCamera();
					}
				}
				//	else
				//	{
				//		//zoomOutCamera();
				//	}
				//}
				if (follower.motion.rotationOffset.y >= 180 && !finish)//완전히 전환
				{
					if (!anistart)
					{
						gameManager.player.GetComponentInChildren<Animator>().SetTrigger("walkTocast");
						gameManager.setStateEnd();
						anistart = true;
					}
					finish = true;
					endingCamera();
				}
				break;
			case GameManager.State.End:
				backCamera();
				//followCamera();
				break;
			case GameManager.State.Finish:
				//엔딩씬 연출
				break;
			case GameManager.State.dead:
				deadCamera();
				gameOverTime += Time.deltaTime;
				if(gameOverTime >5f)
				{
					gameManager.UiController.gameOverScene.SetActive(true);
				}
				break;
		}

	}
	private void IntroCamera()
	{
		//var cameraPos = target.position + target.right * 10;
		//cameraPos.y += 10;
		//transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		////var curpos = transform.rotation;
		////var latpos =  Quaternion.LookRotation(target.position - transform.position);
		////transform.rotation = Quaternion.Slerp(curpos, latpos, Time.deltaTime*10);
		//transform.LookAt(target);
		if (gameManager.UiController.gameStart)
		{
			changetime += Time.deltaTime;
			var extraPos = new Vector3(target.position.x, target.position.y + 9, target.position.z);
			var cameraPos = target.position - target.forward * 15 + target.up * 10;
			transform.position = Vector3.Lerp(curpos, cameraPos, changetime / 1f);
			//var latpos = Quaternion.LookRotation(extraPos - transform.position);
			//transform.rotation = Quaternion.Slerp(curpos, latpos, Time.deltaTime*10);
			transform.LookAt(target);
		}
	}

	private void followCamera()
	{
		
		var extraPos = new Vector3(target.position.x,target.position.y +3,target.position.z);
		var cameraPos = target.position - target.forward * 15* target.localScale.x/2 + target.up *17*target.localScale.x/2;
		//var cameraPos2 = target.position - target.forward * 40 + target.up *47;
		transform.position = cameraPos;
		//var curpos = transform.rotation;
		//var latpos = Quaternion.LookRotation(extraPos - transform.position);
		//transform.rotation = Quaternion.Slerp(curpos, latpos, Time.deltaTime*10);
		transform.LookAt(extraPos);
		

	}
	private void zoomInCaremra()
	{
		var cameraPos = target.position - transform.forward * Mathf.Lerp(17f, 23f, 0f);
		cameraPos.y = 10;
		transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(target);
		
	}
	private void zoomOutCamera()
	{
		var cameraPos = target.position - transform.forward * Mathf.Lerp(23f, 17f, 0f);
		cameraPos.y = 10;
		transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * speed);
		transform.LookAt(target);
		
	}
	private void endingCamera()
	{
		rottime += Time.deltaTime;
		var cameraPos = transform.up;
		cameraPos.y += 10;
		if(rottime < 1f)
		{
			transform.RotateAround(target.position, cameraPos, 180f*Time.deltaTime);
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

	private void topCamera()
	{
		var extraPos = new Vector3(target.position.x - 5, target.position.y, target.position.z+3);
		transform.position = new Vector3(target.position.x+16, target.position.y + 29, target.position.z-15);
		transform.LookAt(extraPos);
		//transform.position = new Vector3(transform.position.x, transform.position.y+5, transform.position.z);
	}
	private void backCamera()
	{
		if (!change)
		{
			transform.position = new Vector3(target.position.x +15, target.position.y+5, target.position.z);
			change = true;
		}
		transform.LookAt(target);
	}
}
