using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class profile : MonoBehaviour
{
    [Serializable]
    public struct Profile
	{
        public Sprite profile1;
        public Sprite profile2;
    }

    [SerializeField] Profile[] allProfile;
    

    public GameObject Avatar1;
    public Transform ProfileOption;
    void Start()
    {
        int N = allProfile.Length;
        for (int i = 0; i < N; i++)
		{
             Instantiate(Avatar1, transform);
            Avatar1.transform.GetComponent<Image>().sprite = allProfile[i].profile1;
            Avatar1.transform.GetChild(0).GetComponent<Image>().sprite = allProfile[i].profile2;
        }
    }

}
