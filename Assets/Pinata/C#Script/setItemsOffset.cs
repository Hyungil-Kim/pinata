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


    public float right;
    public float rightMid;
    public float mid;
    public float leftMid;
    public float left;



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
                splinePositioner.motion.offset = new Vector2(right, splinePositioner.motion.offset.y);
                splinePositioner.motion.rotationOffset = new Vector3(splinePositioner.motion.rotationOffset.x, 180, splinePositioner.motion.rotationOffset.z);
            }
            else if (rightMidOffset)
            {
                splinePositioner.motion.offset = new Vector2(rightMid, splinePositioner.motion.offset.y);
                splinePositioner.motion.rotationOffset = new Vector3(splinePositioner.motion.rotationOffset.x, 180, splinePositioner.motion.rotationOffset.z);
            }
            else if (midOffset)
            {
                splinePositioner.motion.offset = new Vector2(mid, splinePositioner.motion.offset.y);
                splinePositioner.motion.rotationOffset = new Vector3(splinePositioner.motion.rotationOffset.x, 180, splinePositioner.motion.rotationOffset.z);

            }
            else if (leftMidOffset)
            {
                splinePositioner.motion.offset = new Vector2(leftMid, splinePositioner.motion.offset.y);
                splinePositioner.motion.rotationOffset = new Vector3(splinePositioner.motion.rotationOffset.x, 180, splinePositioner.motion.rotationOffset.z);

            }
            else if (leftOffset)
            {
                splinePositioner.motion.offset = new Vector2(left, splinePositioner.motion.offset.y);
                splinePositioner.motion.rotationOffset = new Vector3(splinePositioner.motion.rotationOffset.x, 180, splinePositioner.motion.rotationOffset.z);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
