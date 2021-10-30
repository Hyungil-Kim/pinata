using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject target;
	private Animator animator;
	private Dreamteck.Splines.SplinePositioner spline;
	private float time;
	private float rotateTime = 0f;
	public float speed;
	public float minSpeed;
	public float maxSpeed;
	public Quaternion curquat;
	private bool on;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        target = gameManager.player;
		animator = GetComponent<Animator>();
		spline = gameManager.GetComponentInParent<Dreamteck.Splines.SplinePositioner>();
		curquat = transform.rotation;
	}

    // Update is called once per frame
    void Update()
	{
		time += Time.deltaTime;
		speed = Random.Range(minSpeed, maxSpeed);
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Turn:
				if (time > 1f)
				{
					rotateTime += Time.deltaTime;
					animator.SetTrigger("idleToturn");
					transform.rotation = Quaternion.Slerp(curquat,curquat *Quaternion.Euler(0f,180f,0f),rotateTime/1f);
				}
				
				break;
			case GameManager.State.End:
				if (!animator.GetCurrentAnimatorStateInfo(0).IsName("turn") && !on)
				{
					Debug.Log("1");
					on = true;
				}
				//spline.motion.offset = new Vector2(spline.motion.offset.x - speed, spline.motion.offset.y);
				//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed *Time.deltaTime);
				transform.position += transform.forward * speed * Time.deltaTime;
				break; 
			case GameManager.State.Finish:
				transform.position += transform.forward * speed * Time.deltaTime;
				//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);

				break;
		}

	}
}
