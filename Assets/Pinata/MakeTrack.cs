using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTrack : MonoBehaviour
{
    private GameObject player;
    public GameObject[] roadArr;
    private GameObject[] makeroad;
    private float[] roadArrz;
    private float fixz;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        for(int i =0; i<roadArr.Length;++i )
		{
            roadArr[i].transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z + i *100);
          Instantiate(roadArr[i],gameObject.transform);
            
		}
    }

    void Update()
    {
		
	}
}
