using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public double score;
    public float scale;
    public float speed;
    public float minValue;
    public float maxValue;
    private float time;
    private bool up;
    public int candy;
    private GameManager gameManager;
    private Dreamteck.Splines.SplinePositioner splinePositioner;
    private Dreamteck.Splines.SplineComputer splineComputer;
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    void Start()
    {
        splinePositioner = GetComponent<Dreamteck.Splines.SplinePositioner>();
        splineComputer = GameObject.FindWithTag("Spline").GetComponent<Dreamteck.Splines.SplineComputer>();
        splinePositioner.spline = splineComputer;
        GetComponentInChildren<Animator>().SetTrigger("idleToattack");
    }
    void Update()
    {
        //if (attack)
        //{
        //    GetComponentInChildren<Animator>().SetTrigger("idleToattack");
        //    attack = false;
        //}
        //else if (Vector3.Distance(gameObject.transform.position, gameManager.player.transform.position) < 10f)
        //{
        //    GetComponentInChildren<Animator>().SetTrigger("idleToattack");
        //}
        //else
        //{
        //    GetComponentInChildren<Animator>().SetTrigger("attackToidle");
        //}
        var inputOffset = splinePositioner.motion.offset.x;
        time += Time.deltaTime;
        if (time <= 3f)
        {
            if (inputOffset <= minValue)
            {
                inputOffset = minValue;
                up = true;
            }
            else if (inputOffset >= maxValue)
            {
                inputOffset = maxValue;
                up = false;
                
            }
            if (up)
            {
               
                splinePositioner.motion.rotationOffset = new Vector3(0f, 90f, 0f);
                inputOffset = Mathf.Lerp(minValue, maxValue, time / 1f);
                
            }
            else
            {
                inputOffset = Mathf.Lerp(maxValue, minValue, (time - 1f) / 1f);
                splinePositioner.motion.rotationOffset = new Vector3(0f, 270f, 0f);
                
                if(inputOffset == minValue)
				{
                    time = 0f;
                }
            }
            splinePositioner.motion.offset = new Vector2(inputOffset, splinePositioner.motion.offset.y);
        }

    }

}
