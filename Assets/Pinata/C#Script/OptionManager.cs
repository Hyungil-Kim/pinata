using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
	public UIManager uiManager;
	public GameObject optionPanel;
	public AudioMixer audioMixer;


	public Slider backSlider;
	public Button backSoundUp;
	public Button backSoundDown;

	public Slider effectSlider;
	public Button effectSoundUp;
	public Button effectSoundDown;

	public Button exit;

	void Start()
	{

	}

	void Update()
	{

		AudioControl();
	}

	public void AudioControl()
	{
		float backGroundVolume = backSlider.value;
		float effectVolume = effectSlider.value;

		if (backGroundVolume == -40f)
		{
			audioMixer.SetFloat("backGround", -80);
		}
		else
		{
			audioMixer.SetFloat("backGround", backGroundVolume);
		}
		if (effectVolume == -40f)
		{
			audioMixer.SetFloat("effect", -80);
		}
		else
		{
			audioMixer.SetFloat("effect", effectVolume);
		}
	}
	public void OnClickoffOption()
	{
		optionPanel.SetActive(false);
		uiManager.pause = false;
	}
	public void OnClickVolumeUp(Slider slider)
	{
		float upsize = 5f;
		if (slider.value + upsize < slider.maxValue)
		{
			slider.value += upsize;
		}
		else
		{
			slider.value = slider.maxValue;
		}
	}
	public void OnClickVolumeDown(Slider slider)
	{
		float downsize = 5f;
		if (slider.value - downsize > slider.minValue)
		{
			slider.value -= downsize;
		}
		else
		{
			slider.value = slider.minValue;
		}
	}
}
