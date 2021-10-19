using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject target;
    public GameObject enemy;
    private bool spawn;
    private GameObject[] roads;
    private Dreamteck.Splines.Spline spline;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        target = gameManager.player;
        spawn = true;
        roads = gameManager.roads;
        spline = gameManager.spline;
    }

    // Update is called once per frame
    void Update()
	{
		switch (gameManager.CurrentState)
		{
			case GameManager.State.Start:
                //var newPos = target.transform.position;
                //newPos.x = transform.position.x;
                //newPos.z -= 16;

                //transform.position = newPos;
                ////transform.LookAt(target.transform);
                //transform.rotation = Quaternion.LookRotation(Vector3.back);
                break;
			case GameManager.State.End:
                if (spawn)
                {
                    for (int i = 0; i < roads.Length; i++)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            Instantiate(enemy, new Vector3(), Quaternion.identity);
                        }
                    }
                    spawn = false;
                }
				break;
		}
		
	}
}
