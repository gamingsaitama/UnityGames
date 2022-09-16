using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class OnlineLoginManager : MonoBehaviour
{
    [SerializeField] private Button Facebook;
    [SerializeField] private Button Google;

    private void Awake()
    {

    }

    private void Start()
    {
        Facebook.onClick.AddListener(InitiateFBLogin);
        Google.onClick.AddListener(InitiateGoogleLogin);
    }

    private void OnDestroy()
    {
        Facebook.onClick.RemoveListener(InitiateFBLogin);
        Google.onClick.RemoveListener(InitiateGoogleLogin);
    }

    private void InitiateFBLogin()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            LoadGameScene("FacebookLoginScene");
        }

        else
            FB.Init(InitialisingCallBack, OnHideUnity);
    }

    private void InitiateGoogleLogin()
    {

    }

    private void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    #region FBCallBacks

    private void InitialisingCallBack()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            LoadGameScene("FacebookLoginScene");
        }
        else
            Debug.Log("Failed to Initialize the Facebook SDK");
    }

    #endregion
}
