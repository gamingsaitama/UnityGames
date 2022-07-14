
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;

public class MenuViewItem : MonoBehaviour
{
	/*[Serializable]
	public struct Game
	{
		public string Name;
		public Sprite Icon;

	}

	[SerializeField] Game[] allGames;*/

	public GameObject Pubg;
	public Transform MenuOption;

	private string GameDataUrl = "https://raw.githubusercontent.com/AmDce/UnityGames/main/demoJSON.json";

	void Start()
	{
		//SetMenuData();
		StartCoroutine(SetJsonData());
	}

	/*private void SetMenuData()
	{
		int N = allGames.Length;
		for (int i = 0; i < N; i++)
		{
			var btn = Instantiate(Pubg, MenuOption);
			btn.GetComponent<GameButtonDataHandler>().gameIconImage.sprite = allGames[i].Icon;
			btn.GetComponent<GameButtonDataHandler>().gameName.text = allGames[i].Name;
			//btn.GetComponent<Button>().onClick.AddListener();
		}
	}
	*/
	IEnumerator SetJsonData()
	{
		UnityWebRequest uwr = UnityWebRequest.Get(GameDataUrl);
		yield return uwr.SendWebRequest();

		if (uwr.result == UnityWebRequest.Result.ConnectionError)
		{
			Debug.Log(UnityWebRequest.Result.ConnectionError);
		}
		else
		{
			var json = JSON.Parse(uwr.downloadHandler.text);
			JSONArray jsonObject = json["GameData"] as JSONArray;

			foreach (JSONObject item in jsonObject)
			{
				Debug.Log($"gameid: {item["id"]}");
				Debug.Log($"gameName: {item["gameName"]}");
				Debug.Log($"sceneName: {item["sceneName"]}");
				var btn = Instantiate(Pubg, MenuOption);
				btn.GetComponent<GameButtonDataHandler>().gameName.text = item["gameName"];
			}
		}

	}
}

public class JsonGameData
{
    public List<GameData> GameData;
}

public class GameData
{
    public string id;
    public string gameName;
    public string sceneName;
    public string iconpath;
}



