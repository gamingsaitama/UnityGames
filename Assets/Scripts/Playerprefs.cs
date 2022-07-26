using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Playerprefs : MonoBehaviour
{
    //public GameObject Playername;
    [SerializeField] TMP_InputField Playername;

    // Start is called before the first frame update
    void Start()
    {
        //Username();
        name = PlayerPrefs.GetString("Username");
        Debug.Log(PlayerPrefs.GetString("Username"));

    }

    public void Username()
	{
        PlayerPrefs.SetString("Username",Playername.text);
	}

}
