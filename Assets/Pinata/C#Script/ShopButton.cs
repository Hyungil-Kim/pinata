using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public ShopScript shopScript;
    private Image image;
    private Button button;
    public Texture texture;
    public Sprite ClickedSprite;
    public Sprite DeClickedSprite;
    public bool curClick;
    public bool open;
    
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curClick)
		{
            image.sprite = ClickedSprite;
		}
		else
        {
            image.sprite = DeClickedSprite;
        }
        if(open)
		{
            button.interactable = true;
		}
		else
		{
            button.interactable = false;
		}
    }
   
}
