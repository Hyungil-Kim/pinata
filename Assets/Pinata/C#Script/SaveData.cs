using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{ 
	public static void SavePlayerData(PlayerData data)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.data";
		FileStream stream = new FileStream(path, FileMode.Create);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerData LoadPlayerData()
	{
		string path = Application.persistentDataPath + "/player.data";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();
			return data;
		}
		else
		{
			//Debug.LogError("save file not found in" + path);
			return null;
		}
	}

}