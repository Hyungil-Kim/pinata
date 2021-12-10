using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
	public UIManager uIManager;
	public Button endButton;
	public Button endButtonAD;
	private Vector3 scaleNum;
	private Vector3 changeNum;
	private bool on;
	private float time;
	public float changetime;

	private void Awake()
	{
		scaleNum = endButtonAD.GetComponent<RectTransform>().localScale;
		changeNum = new Vector3(scaleNum.x + 0.2f, scaleNum.y + 0.2f, scaleNum.z + 0.2f);

	}
	private void OnEnable()
	{
		uIManager.animationCamera.SetActive(true);
		uIManager.gameManager.earnGold = 0;
	}
	void Start()
	{

	}
	void Update()
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
		if (time > changetime)
		{
			time = 0f;
		}

		if(SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
		{
			endButtonAD.gameObject.SetActive(false);
		}

	}
	public void OnclickAdButton()
	{
		if (!GoogleMobileAdTest.rewardedAd2.IsLoaded())
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
		GoogleMobileAdTest.OnClickReward2();
		uIManager.gameManager.stageLevel += 1;
		}
		uIManager.gameManager.Save();
	}
	public void OnclickNextbutton()
	{
		if (!GoogleMobileAdTest.interstitial2.IsLoaded())
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
		GoogleMobileAdTest.OnclickInterstitial2();
		}
		uIManager.gameManager.Save();
	}

}
