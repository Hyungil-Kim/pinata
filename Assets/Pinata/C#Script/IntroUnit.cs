using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUnit : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, gameManager.player.transform.position) < 10f)
        {
            GetComponentInChildren<Animator>().SetTrigger("idleToattack");
        }
    }
}
