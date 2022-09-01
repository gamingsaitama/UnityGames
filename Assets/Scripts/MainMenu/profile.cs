using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class profile : MonoBehaviour
{
    [Serializable]
    public struct Profile
	{
        //public Sprite profile1;
        public Sprite profile2;

    }
    
    [SerializeField] Profile[] allProfile;
    
    public GameObject Avatar1;
    public Transform ProfileOption;
    void Start()
    {
        //Instantiate(Resources.Load<Sprite>("Folder/syed"));
        //Debug.Log("LOAD SYED");
        int N = allProfile.Length;
        for (int i = 0; i < N; i++)
		{
             Instantiate(Avatar1, transform);
            //Avatar1.transform.GetComponent<Image>().sprite = allProfile[i].profile1;
            Avatar1.transform.GetComponent<Image>().sprite = allProfile[i].profile2;

            /*}
            for (int i = 0; i < 15; i++)
            {
                Avatar1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("GamesIcon/0");
                Debug.Log("SYED");*/
        }
            





    }

}
