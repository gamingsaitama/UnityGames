
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MenuViewItem : MonoBehaviour
{
	[Serializable]
	public struct Game
	{
		public string Name;
		public Sprite Icon;
		
	}

	[SerializeField] Game[] allGames;

	public GameObject Pubg;
	public Transform MenuOption;

	void Start()
	{
		SetMenuData();
	}

	private void SetMenuData()
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

}
