using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public int playerGold;
	public int currentStage;
	public int mask;
	public int closeLength;
	public string playerSkin;
	public string avatarSkin;
	public bool[] curButton;

	public PlayerData(GameManager gameManager)
	{
		currentStage = gameManager.stageLevel;
		playerGold = gameManager.savegold;
		closeLength = gameManager.UiController.shopPanel.closeLength;
		mask = gameManager.UiController.shopPanel.mask;
		playerSkin = gameManager.player.GetComponentInChildren<SkinnedMeshRenderer>(true).sharedMaterial.mainTexture.name;
		avatarSkin = gameManager.UiController.animationCamera.GetComponentInChildren<SkinnedMeshRenderer>(true).sharedMaterial.mainTexture.name;

	}
	
}
