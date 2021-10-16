using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTrack : MonoBehaviour
{
    public GameObject[] roadArr;
    void Start()
    {
        for(int i =0; i<roadArr.Length;++i )
		{
            roadArr[i].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + i*50);
            Instantiate(roadArr[i],gameObject.transform);
            
		}
    }

    void Update()
    {
        
    }
}
