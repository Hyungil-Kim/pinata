using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Touch touch;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float frontSpeed = 1f;
    private int score;
    private GameManager gameManager;
    public float lift = 1f;
    public float power = 10f;
	// Start is called before the first frame update
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		
	}
	void Start()
    {
        score = gameManager.score;

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameManager.CurrentState)
        {

            case GameManager.State.Start:
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + frontSpeed);
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed, transform.position.y, transform.position.z);
                    }
                }
                break;
                case GameManager.State.End:
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - frontSpeed);
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        transform.position = new Vector3(transform.position.x - touch.deltaPosition.x * speed, transform.position.y, transform.position.z);
                    }
                }
                break;
        }

		
	}
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle" )
        {
            var colscore = collision.transform.GetComponent<Obstacle>().score;
            if(score >= colscore)
			{
                score -= colscore;
                transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
			}
             Debug.Log(score);
            
        }
        if(collision.transform.tag == "Item")
		{
            var colscore = collision.transform.GetComponent<Item>().score;
            score += colscore;
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            Destroy(collision.gameObject);
            Debug.Log(transform.localScale);
            Debug.Log(score);
        }
        if(collision.transform.tag == "ChangeItem")
		{
            gameManager.setStateEnd();
        }
        if(collision.transform.tag == "Enemy")
		{
            var force = collision.gameObject.transform.position - transform.position;
            force.Normalize();
            force.y += lift;
            collision.rigidbody.AddForce(force * power);

        }
    }
	
  
}
