using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 180;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void MovetoX(int x)
	{
        
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
