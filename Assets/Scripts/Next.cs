using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
	 void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
