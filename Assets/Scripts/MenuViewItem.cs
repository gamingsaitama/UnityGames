
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;


public class MenuViewItem : MonoBehaviour
{
	public GameObject Pubg;
	public Transform MenuOption;
	public static MenuViewItem Instance;

	private string GameDataUrl = "https://raw.githubusercontent.com/AmDce/UnityGames/main/demoJSON.json";

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		StartCoroutine(SetJsonData());
		if(Instance != null)
        {
			Instance = this;
		}
	}

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
				var btn = Instantiate(Pubg, MenuOption);
				btn.GetComponent<GameButtonDataHandler>().gameName.text = item["gameName"];
				btn.GetComponent<GameButtonDataHandler>().gameIconImage.sprite = Resources.Load<Sprite>(item["iconpath"]);
				btn.GetComponent<Button>().onClick.AddListener(() =>
				{
					StartCoroutine(LoadGameScene(item["sceneName"], item["isGameInLandscape"]));
				});
			}
		}
	}

	public IEnumerator LoadGameScene(JSONNode GameScene, bool isGameRotated)
	{
		yield return new WaitForSeconds(1f);
		Screen.orientation = isGameRotated ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
		SceneManager.LoadScene(GameScene.Value);
	}
}

