using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ShopScript : MonoBehaviour
{
	public GameManager gameManager;
	public UIManager uIManager;
	public Button[] shopButton;
	public Button goldButton;
	public Button buyButtonAD;
	public Button endButton;
	public Animator animator;
	public ShopButton[] shopButtonComponent;
	public SkinnedMeshRenderer meshRenderer;
	public SkinnedMeshRenderer playerMeshRenderer;
	public TextMeshProUGUI gold;
	public Sprite Button750;
	public Sprite Button1250;
	private int randomNum;
	public int closeLength;
	public int mask;
	private int num;
	private int price;
	//private Vector3 scaleNum;
	//private Vector3 changeNum;
	//private bool on;
	//private float time;
	//public float changetime;
	void Awake()
	{

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
		for (int i = 0; i < this.shopButton.Length; i++)
		{
			int index = i;
			shopButton[index].onClick.AddListener(() => this.OnclickEvent(index));
		}

		if (mask == 0)
		{
			for (int i = 0; i < shopButton.Length; i++)
			{
				if (shopButtonComponent[i].open)
				{
					mask += 1 << i;
				}
				else
				{
					closeLength++;
				}
			}
		}
		else
		{
			for (int i = 0; i < shopButton.Length; i++)
			{
				if ((mask >> i & 1) == 1)
				{
					shopButtonComponent[i].open = true;
				}
				if(playerMeshRenderer.material.mainTexture == shopButtonComponent[i].texture)
				{
					shopButtonComponent[i].curClick = true;
				}
				else
				{
					shopButtonComponent[i].curClick = false;
				}
			}
		}
	}

	void Update()
	{
		gold.text = gameManager.changeUnit(uIManager.gameManager.savegold);
		if(closeLength == 2)
		{
			buyButtonAD.image.sprite = Button750;
			price = 750;
		}
		else if(closeLength <= 1)
		{
			buyButtonAD.image.sprite = Button1250;
			price = 1250;
		}
		else
		{
			price = 250;
		}
	}

	public void OnclickEvent(int index)
	{
		for (int i = 0; i < shopButton.Length; i++)
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
		if (closeLength != 0)
		{
			if (gameManager.savegold >= price)
			{
				gameManager.savegold -= price;
				shopButtonComponent[num].open = true;
				closeLength--;
				mask += 1 << num;
				gameManager.Save();
			}
		}
	}
	public void OnclickGoldButton()
	{
		if(GoogleMobileAdTest.rewardedAd.IsLoaded())
		{
			GoogleMobileAdTest.OnClickReward();
		}
		gameManager.Save();
	}
	public void OnclickUnlockbutton()
	{
		randomNum = Random.Range(0, closeLength);
		int count = -1;
		for (int i = 0; i < shopButton.Length; i++)
		{
			if ((mask >> i & 1) == 0)
			{
				count++;
				if (count == randomNum)
				{
					num = i;
					break;
				}
			}
		}

		setInterectable();
	}
	public void OnClickBackButton()
	{
		gameObject.SetActive(false);
		uIManager.startScene.SetActive(true);
		uIManager.optionButton.gameObject.SetActive(true);
		gameManager.Save();
	}


}
