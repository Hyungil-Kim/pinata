using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScroll : MonoBehaviour
{
    Touch touch;
    private RectTransform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
    //    if (Input.touchCount > 0)
    //    {
    //        touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Moved)
    //        {
    //            if (pos.anchoredPosition.x <= -27f)
				//{
    //                pos.anchoredPosition = new Vector2(-27f, pos.anchoredPosition.y);

    //            }
    //            if(pos.anchoredPosition.x >= 87f)
				//{
    //                pos.anchoredPosition = new Vector2(87f, pos.anchoredPosition.y);
    //            }
    //            var inputOffset = pos.anchoredPosition.x + touch.deltaPosition.x * 1f;
    //            pos.anchoredPosition = new Vector2(inputOffset, pos.anchoredPosition.y);
    //        }
    //    }
    }
}
