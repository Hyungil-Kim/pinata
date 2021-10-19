using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Touch touch;

	private int score;
	private GameManager gameManager;
	public float lift = 1f;
	public float power = 10f;
	private Dreamteck.Splines.SplineFollower player;
	private Dreamteck.Splines.Spline spline;
	// Start is called before the first frame update
	private void Awake()
	{
		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

	}
	void Start()
	{
		score = gameManager.score;
		player = gameManager.player.GetComponent<Dreamteck.Splines.SplineFollower>();
		spline = gameManager.spline;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Start:
				if (Input.touchCount > 0)
				{
					touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Moved)
					{
						var inputOffset = player.motion.offset.x + touch.deltaPosition.x * 0.1f;
						if (inputOffset > 21f)
						{
							inputOffset = 21f;
						}
						if (inputOffset < -21f)
						{
							inputOffset = -21f;
						}
						player.motion.offset = new Vector2(inputOffset, 0);
					}
				}
				break;
			case GameManager.State.End:
				if (Input.touchCount > 0)
				{
					touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Moved)
					{
						var inputOffset = player.motion.offset.x - touch.deltaPosition.x * 0.1f;
						if (inputOffset > 21f)
						{
							inputOffset = 21f;
						}
						if (inputOffset < -21f)
						{
							inputOffset = -21f;
						}
						player.motion.offset = new Vector2(inputOffset, 0);

					}
				}
				break;
		}

	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Obstacle")
		{
			var colscore = collision.transform.GetComponent<Obstacle>().score;
			if (score >= colscore)
			{
				score -= colscore;
				transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
			}
			Debug.Log(score);
		}
		if (collision.transform.tag == "Item")
		{
			var colscore = collision.transform.GetComponent<Item>().score;
			score += colscore;
			transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
			Destroy(collision.gameObject);
			Debug.Log(transform.localScale);
			Debug.Log(score);
		}
		if (collision.transform.tag == "ChangeItem")
		{
			gameManager.setStateEnd();
			gameManager.setEnemyOn();
		}
		if (collision.transform.tag == "Enemy")
		{
			var force = collision.gameObject.transform.position - transform.position;
			force.Normalize();
			force.y += lift;
			collision.rigidbody.AddForce(force * power);

		}
	}


}
