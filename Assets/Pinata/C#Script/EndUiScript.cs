using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndUiScript : MonoBehaviour
{
	public UIManager uIManager;
	public GameObject poor;
	public GameObject good;
	public GameObject great;
	public GameObject awesome;
	public Button endButton;
	public Button endButtonAD;
	public TextMeshProUGUI endScore;
	public Animator animator;
	public Image finalImage;
	private Vector3 scaleNum;
	private Vector3 changeNum;
	private bool on;
	private float time;
	private float opentime;
	public float changetime;
	private AudioSource audioSource;
	public AudioClip endSound;
	private bool endbool;
	private Scene scene;
	// Start is called before the first frame update
	private void Awake()
	{
		scaleNum = endButtonAD.GetComponent<RectTransform>().localScale;
		changeNum = new Vector3(scaleNum.x + 0.2f, scaleNum.y + 0.2f, scaleNum.z + 0.2f);
		audioSource = GetComponent<AudioSource>();
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
		if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
		{
			finalImage.gameObject.SetActive(true);
		}
		else
		{
			time += Time.deltaTime;
			opentime += Time.deltaTime;
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
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cast Spell") && !endbool)
			{
				audioSource.PlayOneShot(endSound);
				endbool = true;
			}
			if (opentime >= 2f)
			{
				endButton.gameObject.SetActive(true);
			}
		}
	}
	public void OnclickAdButton()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
			{
				SceneManager.LoadScene(0);
			}
			else
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
		else
		{
		GoogleMobileAdTest.OnClickReward2();
		}
		uIManager.gameManager.savegold += uIManager.gameManager.earnGold;
		uIManager.gameManager.stageLevel += 1;
		uIManager.gameManager.Save();
	}
	public void OnclickNextButton()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
		GoogleMobileAdTest.OnclickInterstitial();
		}
		uIManager.gameManager.savegold += uIManager.gameManager.earnGold;
		uIManager.gameManager.stageLevel += 1;
		uIManager.gameManager.Save();
	
	}
}
