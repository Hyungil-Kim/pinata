using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopScript : MonoBehaviour
{
	public GameManager gameManager;
    public UIManager uIManager;
	public Button[] shopButton;
    public Button goldButton;
    public Button buyButtonAD;
	public Animator animator;
	public Sprite selectedImage;
	public Sprite deSelectedImage;
	public ShopButton[] shopButtonComponent;
	public SkinnedMeshRenderer meshRenderer;
	public SkinnedMeshRenderer playerMeshRenderer;
	private int randomNum;
	private int num;
    //private Vector3 scaleNum;
    //private Vector3 changeNum;
    //private bool on;
    //private float time;
    //public float changetime;
    void Awake()
    {

		//scaleNum = endButtonAD.GetComponent<RectTransform>().localScale;
		//changeNum = new Vector3(scaleNum.x + 0.2f, scaleNum.y + 0.2f, scaleNum.z + 0.2f);
	}

    private void OnEnable()
    {
        uIManager.animationCamera.SetActive(true);
    }
	private void Start()
	{
		for (int i = 0; i < shopButton.Length; i++)
		{
			shopButtonComponent[i] = shopButton[i].GetComponent<ShopButton>();
		}
		for (int i =0; i<this.shopButton.Length;i++)
		{
			int index = i;
			shopButton[index].onClick.AddListener(() => this.OnclickEvent(index));
		}
	}

	void Update()
    {
		 randomNum = Random.Range(0, shopButton.Length);
	}

	//   void PopUpButton()
	//{
	//	time += Time.deltaTime;
	//	if (!on)
	//	{
	//		endButtonAD.GetComponent<RectTransform>().localScale = Vector3.Lerp(scaleNum, changeNum, time / changetime);
	//		if (time >= changetime)
	//		{
	//			time = 0f;
	//			on = true;
	//		}
	//	}
	//	else
	//	{
	//		endButtonAD.GetComponent<RectTransform>().localScale = Vector3.Lerp(changeNum, scaleNum, time / changetime);
	//		if (time >= changetime)
	//		{
	//			time = 0f;
	//			on = false;
	//		}
	//	}
	//	if (time > changetime)
	//	{
	//		time = 0f;
	//	}
	//}
	
	public void OnclickEvent(int index)
	{
		for(int i =0; i< shopButton.Length;i++)
		{
			shopButtonComponent[i].curClick = false;
		}
		shopButtonComponent[index].curClick = true;
		meshRenderer.material.mainTexture = shopButtonComponent[index].texture;
		playerMeshRenderer.material.mainTexture = shopButtonComponent[index].texture;
		
		animator.SetTrigger("ClickShop");
	}
	public void setInterectable()
	{
		if (gameManager.gold >= 9000)
		{
			if (!shopButtonComponent[randomNum].open)
			{
				gameManager.gold -= 9000;
				shopButtonComponent[randomNum].open = true;
			}
			else
			{
				if(randomNum+1 < randomNum)
				{
					randomNum++;
				}
				else
				{
					randomNum = 1;
				}
			
			}
		}
	}
	public void OnclickGoldButton()
	{
		//골드얻기
	}
	public void OnclickUnlockbutton()
	{
		setInterectable();
		//해제
	}



}
