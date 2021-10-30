using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	// Start is called before the first frame update
	private void Awake()
	{
		scaleNum = endButtonAD.GetComponent<RectTransform>().localScale;
		changeNum = new Vector3(scaleNum.x + 0.2f, scaleNum.y + 0.2f, scaleNum.z + 0.2f);

	}
	private void OnEnable()
	{
		uIManager.animationCamera.SetActive(true);
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
	}
	public void OnclickAdButton()
	{
		//광고출력
		//다음씬 불러오기
	}
	public void OnclickNextbutton()
	{
		//다음씬 불러오기
	}

}
