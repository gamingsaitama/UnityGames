
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Demo : MonoBehaviour
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
	//public GameObject Text;



	void Start()
	{
		int N = allGames.Length;
		for (int i = 0; i < N; i++)
		{
			Instantiate(Pubg, MenuOption);
			//Instantiate(Text, MenuOption);
			Pubg.gameObject.transform.GetComponent<Image>().sprite = allGames[i].Icon;
			Pubg.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = allGames[i].Name;
			//Pubg.gameObject.GetComponent<Text>().text = allGames[i].Name;
		}
	}
}
