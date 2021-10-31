using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUiScript : MonoBehaviour
{
	public UIManager uIManager;
	public GameObject poor;
	public GameObject good;
	public GameObject great;
	public GameObject awesome;
	public Button endButton;
	public Button endButtonAD;
	public Text endScore;
	public Animator animator;
	private Vector3 scaleNum;
	private Vector3 changeNum;
	private bool on;
	private float time;
	public float changetime;

	// Start is called before the first frame update
	private void Awake()
	{
		scaleNum = endButtonAD.GetComponent<RectTransform>().localScale;
		changeNum = new Vector3(scaleNum.x + 0.2f, scaleNum.y + 0.2f, scaleNum.z + 0.2f);

	}
	private void OnEnable()
	{
		uIManager.animationCamera.SetActive(true);
		endScore.text = $"YOU GET {uIManager.gameManager.earnGold}";
		if (uIManager.gameManager.percentScore >= 0.8)
		{
			awesome.SetActive(true);
		}
		else if (uIManager.gameManager.percentScore >= 0.5)
		{
			great.SetActive(true);
		}
		else if (uIManager.gameManager.percentScore >= 0.1)
		{
			good.SetActive(true);
		}
		else
		{
			poor.SetActive(true);
		}
		animator.SetFloat("Score", ((float)uIManager.gameManager.percentScore));
	}
	public void Update()
	{
		time += Time.deltaTime;
		if (!on)
		{
			endButtonAD.GetComponent<RectTransform>().localScale = Vector3.Lerp(scaleNum, changeNum, time / changetime);
			if (time >= changetime)
			{
				time = 0f;
				on = true;
			}
		}
		else
		{
			endButtonAD.GetComponent<RectTransform>().localScale = Vector3.Lerp(changeNum, scaleNum, time / changetime);
			if (time >= changetime)
			{
				time = 0f;
				on = false;
			}
		}
		if(time > changetime)
		{
			time = 0f;
		}
	}
	public void OnclickAdButton()
	{
		//광고출력
		OnclickNextButton();
		//재화 두배
	}
	public void OnclickNextButton()
	{
		//다음씬 불러오기
	}

}
