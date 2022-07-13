using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next : MonoBehaviour
{
    public GameObject SettingOption;
	public GameObject MenuOption;
	public AudioSource SettingButton;
	public AudioSource BackButton;
	public void ButtontoSetting()
	{
		SettingOption.SetActive(true);
		

	}
	public void ButtontoMenu()
	{
		SettingOption.SetActive(false);
		MenuOption.SetActive(true);
	}
	public void TochTOSound()
	{
		SettingButton.Play();
	}
	public void BackTochToSound()
	{
		BackButton.Play();
	}
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
