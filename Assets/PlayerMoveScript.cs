using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMoveScript : MonoBehaviour
{
    private Touch touch;
	[SerializeField]
    private float speed;
	private float frontSpeed = 1f;
	private void Start()
	{
		speed = 0.01f;
	}
	private void Update()
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
}

