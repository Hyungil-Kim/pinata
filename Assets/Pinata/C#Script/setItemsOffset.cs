using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setItemsOffset : MonoBehaviour
{
    private GameManager gameManager;
    public bool rightOffset = false;
    public bool rightMidOffset = false;
    public bool midOffset = false;
    public bool leftMidOffset = false;
    public bool leftOffset = false;
    private Dreamteck.Splines.SplinePositioner[] splinePositioners;
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        //spline = gameManager.spline;
        splinePositioners = GetComponentsInChildren<Dreamteck.Splines.SplinePositioner>();
    }
	void Start()
    {
        foreach (var splinePositioner in splinePositioners)
        {
            if (rightOffset)
            {
                splinePositioner.motion.offset = new Vector2(3.5f, splinePositioner.motion.offset.y);
            }
            else if (rightMidOffset)
            {
                splinePositioner.motion.offset = new Vector2(2f, splinePositioner.motion.offset.y);
            }
            else if (midOffset)
            {
                splinePositioner.motion.offset = new Vector2(0f, splinePositioner.motion.offset.y);
            }
            else if (leftMidOffset)
            {
                splinePositioner.motion.offset = new Vector2(-2f, splinePositioner.motion.offset.y);
            }
            else if (leftOffset)
            {
                splinePositioner.motion.offset = new Vector2(-3.5f, splinePositioner.motion.offset.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
