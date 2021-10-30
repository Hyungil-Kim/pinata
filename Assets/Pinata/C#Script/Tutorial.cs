using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Slider slider; 
    private float time = 0f;
    private bool up = true;
    private Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= 3f)
        {
            if (slider.value <= 0)
            {
                slider.value = 0f;
                up = true;
            }
            else if (slider.value >= 1)
            {
                slider.value = 1f;
                up = false;
            }
            if (up)
            {
                slider.value = Mathf.Lerp(0, 1, time/1f);
            }
            else
            {
                slider.value = Mathf.Lerp(1, 0, (time-1f) /1f);
            }
        }
		else
		{
            gameObject.SetActive(false);
		}
        if(Input.touchCount > 0)
		{
            if(touch.phase == TouchPhase.Began)
			{
                gameObject.SetActive(false);
			}
		}
    }
}
