using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Touch touch;
	private GameManager gameManager;
	private UIManager ui;
	private Dreamteck.Splines.SplineFollower player;
	private Dreamteck.Splines.SplineComputer splineComputer;
	private Rigidbody pRigidbody;
	public ParticleSystem hitparticle;
	public ParticleSystem hurtparticle;
	public ParticleSystem lastparticle;
	private AudioSource audioSource;
	private Animator animator;
	public AudioClip itemSound;
	public AudioClip hurtSound;
	public AudioClip attackSound;
	public AudioClip endWinSound;
	public AudioClip endLoseSound;
	private Vector3 particleScale;
	public GameObject mapobj;

	public double score;
	public int gold;
	public float lift;
	public float power;
	public float deadPower;
	private double scoreMove = 0f;
	public bool gameStart;
	private float offsety;
	private float time;
	private float endingtime; 
	private float life = 2;
	private bool idle;
	public float limitScore = 500;
	private bool playingParticle;
	private float particleTime;
	private bool goalSound;
	private Vector3 curScale;
	private float scaleTime;
	private bool startScaleTime;
	private float timeTimer;
	// Start is called before the first frame update
	private void Awake()
	{
		player = GetComponent<Dreamteck.Splines.SplineFollower>();
		splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		player.spline = splineComputer;
		animator = GetComponentInChildren<Animator>();
		audioSource = GetComponent<AudioSource>();
		pRigidbody = GetComponent<Rigidbody>();
	}
	void Start()
	{
		ui = gameManager.UiController.GetComponent<UIManager>();
		score = gameManager.score;
		offsety = player.motion.offset.y;
	}
	private void Update()
	{
		if (score == 0)
		{
			scoreMove = 1 - 0.05;
		}
		else
		{
			scoreMove = 1 - (score / 10000);
		}
		particleScale = new Vector3(transform.localScale.x * 0.4f, transform.localScale.y * 0.4f, transform.localScale.z * 0.4f);
		hitparticle.transform.localScale = particleScale;
		hurtparticle.transform.localScale = particleScale;
		particleTime += Time.deltaTime;
		if (particleTime > 0.4f)
		{
			playingParticle = true;
		}
		if (player.followSpeed > 0)
		{
			if (!audioSource.isPlaying)
			{
				//발소리 이슈
			}
		}
		if (startScaleTime)
		{
			timeTimer += Time.deltaTime;
			scaleTime += Time.deltaTime;
			var twice = curScale * 2;
			if (curScale != twice)
			{
				if (timeTimer > 0.3f)
				{
					transform.localScale = Vector3.Lerp(curScale, twice, scaleTime / 1f);
					timeTimer = 0;
				}
			}
			if(scaleTime > 1f)
			{
				startScaleTime = false;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Intro:
				player.followSpeed = 0;
				animator.speed = 0;
				gameStart = ui.gameStart;
				if (gameStart)
				{
					time += Time.deltaTime;
					offsety = Mathf.Lerp(1f, -2f, time /0.5f);//높이 
					player.motion.offset = new Vector2(0, offsety);
				}
				if (offsety == -2f)
				{
					animator.speed = 1;
				}
				if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
				{
					gameManager.CurrentState = GameManager.State.Start;
				}
				break;
			case GameManager.State.Start:

				if (Input.touchCount > 0)
				{
					touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Moved)
					{
						var inputOffset = player.motion.offset.x + touch.deltaPosition.x * 0.05f;
						if (inputOffset >= 5.5f)
						{
							inputOffset = 5.5f;
						}
						if (inputOffset <= -3.5f)
						{
							inputOffset = -3.5f;
						}
						player.motion.offset = new Vector2(inputOffset, player.motion.offset.y);
					}
				}

				break;
			case GameManager.State.Turn:
				{
					player.followSpeed = 0;
					if (player.motion.rotationOffset.y >= 180)
					{
					}
					else
					{
						player.motion.rotationOffset += new Vector3(0, 1f, 0);
					}
				}
				break;
			case GameManager.State.End:
				if (player.GetPercent() < scoreMove)
				{
					player.followSpeed = 0;
					//날라가기?
				}
				if (Input.touchCount > 0)
				{
					touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Moved)
					{
						var inputOffset = player.motion.offset.x - touch.deltaPosition.x * 0.05f;
						if (inputOffset >= 5.5f)
						{
							inputOffset = 5.5f;
						}
						if (inputOffset <= -3.5f)
						{
							inputOffset = -3.5f;
						}
						player.motion.offset = new Vector2(inputOffset, player.motion.offset.y);
					}
				}
				if (player.followSpeed <= 5 && !idle)
				{
					player.GetComponentInChildren<Animator>().SetTrigger("runToidle");
					foreach (var enemy in gameManager.enemys)
					{
						enemy.GetComponent<Animator>().SetTrigger("runTodance");//변경 회전해야할수도
					}
					idle = true;
				}
				if (player.followSpeed <= 5)
				{
					endingtime += Time.deltaTime;
					if (endingtime > 5f)
					{
						gameManager.setStateFinish();
					}
				}
				break;
			case GameManager.State.Finish:
				{
					if (!goalSound)
					{
						if (0.5 < gameManager.percentScore)
						{
							audioSource.PlayOneShot(endWinSound);
							goalSound = true;
						}
						else
						{
							audioSource.PlayOneShot(endLoseSound);
							goalSound = true;
						}
					}
				}
				break;
		}

	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Obstacle")
		{
			if (score < limitScore)
			{
				hurtparticle.transform.position = collision.transform.position;
				hurtparticle.Play();
				audioSource.PlayOneShot(hurtSound);
				var colscore = collision.transform.GetComponent<Obstacle>().score;
				if(colscore >= score)
				{
					colscore = 0;
				}
				else
				{
				score -= colscore;
				}
				transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f); //크기키우기
				// 최소크기 최대크기 작성하기
				if (score <= 0)
				{
					life--;
					Debug.Log($"life = { life }");
				}
				if (life == 0)
				{
					playerForce(collision);
				}

				Debug.Log(score);
			}
		}
		if (collision.transform.tag == "Item")
		{
			var colscore = collision.transform.GetComponent<Item>().score;
			score += colscore;
			transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
			curScale = transform.localScale;
			hitparticle.Play();
			audioSource.PlayOneShot(itemSound);
			gold += 1;
			Destroy(collision.gameObject);
			Debug.Log(transform.localScale);
			Debug.Log(score);
		}
		if (collision.transform.tag == "ChangeItem")
		{
			startScaleTime = true;
			Destroy(collision.gameObject);
			mapobj.SetActive(false);
			gameManager.setStateTurn();
			gameManager.setEnemyOn();


		}
		if (collision.transform.tag == "Enemy")
		{
			if (score > limitScore)
			{
				collision.gameObject.GetComponent<Animator>().enabled = false;
				var force = collision.gameObject.transform.position - transform.position;
				force.Normalize();
				force.y += lift;
				if (playingParticle)
				{
					lastparticle.Play();
					playingParticle = false;
					particleTime = 0;
				}
				audioSource.PlayOneShot(attackSound);
				collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				collision.rigidbody.AddForce(force * power);
			}
			else
			{
				//날라가기
				playerForce(collision);
			}
		}
		
	}
	//날라기기 함수
	private void playerForce(Collision collision)
	{
		gameManager.setStatedead();
		player.enabled = false;
		pRigidbody.constraints = RigidbodyConstraints.None;
		var force = transform.position - collision.gameObject.transform.position;
		force.Normalize();
		force.y += lift;
		//if (playingParticle)
		//{
		//	lastparticle.Play();
		//	playingParticle = false;
		//	particleTime = 0;
		//}
		//audioSource.PlayOneShot(attackSound);
		gameObject.layer = 9;
		pRigidbody.AddForce(force * deadPower);
	}

}
