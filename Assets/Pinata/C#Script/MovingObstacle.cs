using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    public float speed;
    public float minValue;
    public float maxValue;
    private float moveTime;
    private bool up;

    void Update()
    {
        Move();
    }
	private void Move()
	{
        var inputOffset = splinePositioner.motion.offset.x;
        moveTime += Time.deltaTime;
        if (moveTime <= 3f)
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
                inputOffset = Mathf.Lerp(minValue, maxValue, moveTime / 1f);
            }
            else
            {
                inputOffset = Mathf.Lerp(maxValue, minValue, (moveTime - 1f) / 1f);
                splinePositioner.motion.rotationOffset = new Vector3(0f, 270f, 0f);

                if (inputOffset == minValue)
                {
                    moveTime = 0f;
                }
            }
            splinePositioner.motion.offset = new Vector2(inputOffset, splinePositioner.motion.offset.y);
        }
    }
}
