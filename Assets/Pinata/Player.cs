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
    private GameObject obstacle;
    private float time;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {

        if (hit == true)
        {
            time += Time.deltaTime;
        }
        if (time > 1f)
        {
            time = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed, transform.position.y, transform.position.z);
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + frontSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle" && hit == false)
        {
            score -= collision.gameObject.GetComponent<Obstacle>().score;
            hit = true;
            Debug.Log(score);
        }

    }
}
