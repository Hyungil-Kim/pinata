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
	public ParticleSystem hurtparticle2;
	public ParticleSystem lastparticle;
	private AudioSource audioSource;
	private Animator animator;
	public AudioClip itemSound;
	public AudioClip hurtSound;
	public AudioClip eatSound;
	public AudioClip attackSound;
	public AudioClip sigeupSound;
	public AudioClip castSpellSound;
	public AudioClip bloodySound;

	private Vector3 particleScale;
	public GameObject mapobj;
	public GameObject rope;
	[SerializeField]
	private float minMove;
	[SerializeField]
	private float maxMove;

	public double score;
	public int gold;
	public float lift;
	public float power;
	public float TrainPower;
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
	private float soundTime;
	private bool goalSound;
	private Vector3 curScale;
	private float scaleTime;
	private bool startScaleTime;
	private float timeTimer;
	private bool start;
	private bool spellsound;
	private bool playeringSound;
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
		particleScale = new Vector3(transform.localScale.x * 0.3f, transform.localScale.y * 0.3f, transform.localScale.z * 0.3f);
		hitparticle.transform.localScale = particleScale;
		hurtparticle.transform.localScale = particleScale;
		particleTime += Time.deltaTime;
		soundTime += Time.deltaTime;
		if (particleTime > 0.4f)
		{
			playingParticle = true;
		}
		if(soundTime > 0.2f)
		{
			playeringSound = true;
		}
		if (player.followSpeed > 0)
		{

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
					audioSource.PlayOneShot(sigeupSound);
				}
			}
			if(scaleTime > 1f)
			{
				startScaleTime = false;
			}
		}
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cast Spell") && !spellsound)
		{
			Debug.Log("1");
			audioSource.PlayOneShot(castSpellSound);
			spellsound = true;
		}
	}
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
					offsety = Mathf.Lerp(6f, 3.5f, time / 0.5f);
					player.motion.offset = new Vector2(0, offsety);
					if (!start)
					{
						Destroy(rope);
						start = true;
					}
				}
					if (offsety == 3.5f)
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
						if (inputOffset >= maxMove)
						{
							inputOffset = maxMove;
						}
						if (inputOffset <= minMove)
						{
							inputOffset = minMove;
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
				}
				if (Input.touchCount > 0)
				{
					touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Moved)
					{
						var inputOffset = player.motion.offset.x - touch.deltaPosition.x * 0.05f;
						if (inputOffset >= maxMove)
						{
							inputOffset = maxMove;
						}
						if (inputOffset <= minMove)
						{
							inputOffset = minMove;
						}
						player.motion.offset = new Vector2(inputOffset, player.motion.offset.y);
					}
				}
				if (player.followSpeed <= 5 && !idle)
				{
					player.GetComponentInChildren<Animator>().SetTrigger("runToidle");
					foreach (var enemy in gameManager.enemys)
					{
						enemy.GetComponent<Animator>().SetTrigger("runTodance");
					}
					idle = true;
				}
				if (player.followSpeed <= 5)
				{
					endingtime += Time.deltaTime;
					if (endingtime > 1f)
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
							goalSound = true;
						}
						else
						{
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
			{
				audioSource.PlayOneShot(hurtSound);
				animator.SetTrigger("runTodamage");
				if (collision.transform.GetComponent<Obstacle>() != null)
				{
					var colscore = collision.transform.GetComponent<Obstacle>().score;
					var colscale = collision.transform.GetComponent<Obstacle>().scale;
					hurtparticle.transform.position = collision.transform.position;
					hurtparticle.Play();
					

					if (colscore >= score)
					{
						score = 0;
					}
					else
					{
						score -= colscore;
					}
					if (transform.localScale.x -colscale > 1f)
					{
						transform.localScale -= new Vector3(colscale, colscale, colscale);
					}
					else
					{
						transform.localScale = new Vector3(1f,1f,1f);
					}
					if (score <= 0)
					{
						life--;
						Debug.Log($"life = { life }");
					}
					if (life == 0)
					{
						playerForce(collision);
					}
				}
				if (collision.transform.GetComponent<MovingObstacle>() != null)
				{
					var colscore = collision.transform.GetComponent<MovingObstacle>().score;
					var colscale = collision.transform.GetComponent<MovingObstacle>().scale;
					hurtparticle.transform.position = collision.transform.position;
					hurtparticle2.Play();
					if (colscore >= score)
					{
						score = 0;
					}
					else
					{
						score -= colscore;
					}
					if (transform.localScale.x - colscale > 1f)
					{
						transform.localScale -= new Vector3(colscale, colscale, colscale);
					}
					else
					{
						transform.localScale = new Vector3(1f, 1f, 1f);
					}
					if (score <= 0)
					{
						life--;
						Debug.Log($"life = { life }");
					}
					if (life == 0)
					{
						playerForce(collision);
					}
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
			audioSource.PlayOneShot(eatSound);
			gold += 1;
			Destroy(collision.gameObject);
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
			//if (score > limitScore)
			{
				collision.gameObject.GetComponent<Dreamteck.Splines.SplineFollower>().enabled = false;
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
				if (playeringSound)
				{
					audioSource.PlayOneShot(attackSound);
					audioSource.PlayOneShot(bloodySound);
					playeringSound = false;
					soundTime = 0f;
				}
				
				collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				collision.rigidbody.AddForce(force * power);
			}
		}
		if (collision.transform.tag == "Train")
		{
			{
				var col = collision.transform.parent;
				if (collision.gameObject.GetComponentInParent<Animator>().enabled)
				{
				collision.gameObject.GetComponentInParent<Animator>().enabled = false;
				}
				var colsChild = col.GetComponentsInChildren<Rigidbody>();
				var force = collision.gameObject.transform.position - transform.position;
				force.Normalize();
				force.y += lift;
				for (int i = 0; i < colsChild.Length; i++)
				{
					colsChild[i].isKinematic = false;
				}
				collision.rigidbody.AddForce(force * TrainPower);
			}
		}
	}
	private void playerForce(Collision collision)
	{
		gameManager.setStatedead();
		player.enabled = false;
		pRigidbody.constraints = RigidbodyConstraints.None;
		var force = transform.position - collision.gameObject.transform.position;
		force.Normalize();
		force.y += lift;
		gameObject.layer = 8;
		pRigidbody.AddForce(force * deadPower);
	}

}
