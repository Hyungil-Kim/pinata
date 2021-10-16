using UnityEngine;


public class MovingCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);
    private float dir;
    private float currentDgree;

    void Update()
    {
        if (target != null)
        {
          
            transform.position = target.position + offset;
           
            if (Input.touchCount == 2)
            {
                var touch0 = Input.touches[0];
                var touch1 = Input.touches[1];

                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                    return;

                var touch0PrevPos = touch0.position - touch0.deltaPosition;
                var touch1PrevPos = touch1.position - touch1.deltaPosition;

                var prevDir = touch1PrevPos - touch0PrevPos;
                var currDir = touch1.position - touch0.position;

                var dot = Vector3.Dot(Vector2.up, prevDir.normalized);
                var prevDegree = Mathf.Acos(dot) * Mathf.Rad2Deg;

                dot = Vector3.Dot(Vector2.up, currDir.normalized);
                var currdegree = Mathf.Acos(dot) * Mathf.Rad2Deg;

                currentDgree += currdegree - prevDegree;
            }
            transform.LookAt(target);
            transform.Rotate(0f, 0f, currentDgree);
        }

    }
}