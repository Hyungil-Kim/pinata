using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDamaged : MonoBehaviour
{
    private GameObject monster;
    private int playerScore;
    private float time;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = GetComponent<CharacterStats>().score;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)
        {
            time += Time.deltaTime;
        }
        if (time > 1f)
		{
            time = 0;
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Monster" && hit == false)
		{
			playerScore -= collision.gameObject.GetComponent<CharacterStats>().score;
			hit = true;
		Debug.Log(playerScore);
		}
			
	}
	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.tag.Equals("Monster") && hit == false)
	//	{
	//		score -= other.GetComponent<CharacterStats>().score;
	//		hit = true;
	//	}
	//		Debug.Log("hit");
	//}
	
}
