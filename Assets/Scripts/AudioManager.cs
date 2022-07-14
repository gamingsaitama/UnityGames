using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Space]
	[Header("Audio Sources and Data")]
	[SerializeField] List<AudioStatesHandler> audioData;

	public static AudioManager Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

    private void Start()
    {
		Debug.Log($"_audioStatesHandler --> {audioData == null} {audioData.Count}");
	}

	public GameObject SettingOption;
	public GameObject MenuOption;



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
		//SettingButtonClip.Play();
	}
	public void BackTochToSound()
	{
		//BackButtonClip.Play();
	}


	public AudioSource _localAudioSource;
	public void PlaySFXAudio(AudioStates audioStates, bool isOnLoop = false)
    {
        //Debug.Log($"_audioStatesHandler --> {audioData.Count}");

		if (audioData != null && audioData.Count > 0)
        {
			Debug.Log($"_audioStatesHandler --> {audioData != null}");
			foreach (AudioStatesHandler item in audioData)
			{
				if(item.audioStates == audioStates)
				{
                    _localAudioSource = item.audioSource;
                    _localAudioSource.loop = isOnLoop;
					_localAudioSource.clip = item.audioClip;
					_localAudioSource.PlayOneShot(item.audioClip);
				}
			}
        }

		if (_localAudioSource != null && _localAudioSource.clip != null && !_localAudioSource.isPlaying)
        {
			_localAudioSource.clip = null;
        }
    }
}

[System.Serializable]
public class AudioStatesHandler
{
    public AudioSource audioSource;
	public AudioClip audioClip;
	public AudioStates audioStates;
}

public enum AudioStates
{
	BGmusic,
	ButtonClick,
	OnKill,
	OnGameEnd,
	OnGameInProgress
}