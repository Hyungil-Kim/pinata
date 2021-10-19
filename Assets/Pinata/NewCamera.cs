using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    private GameManager gameManager;
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		
	}
	private void Start()
    {
		target = gameManager.player.transform;
    }
    void Update()
    {
		var camerPos = target.transform.position - target.transform.forward * 23;
		camerPos.y += 10;
		transform.position = camerPos;
		transform.LookAt(target);
	}
}
