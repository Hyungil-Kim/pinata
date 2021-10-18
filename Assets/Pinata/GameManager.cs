using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    private GameObject[] obstacles;
    private GameObject[] enemys;
    private GameObject[] items;
    private GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        items  = GameObject.FindGameObjectsWithTag("Item");
        Camera = GameObject.FindWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
