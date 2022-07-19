using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LoginManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject RegisterPanel;
    public GameObject logInPanel;

    [Header("RegisterInputFields")]
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_InputField gmail;
    [SerializeField] TMP_InputField password;
    [SerializeField] TMP_InputField confirmPassword;

    [Header("LogInInputs")]
    [SerializeField] TMP_InputField Logingmail;
    [SerializeField] TMP_InputField LoginPassword;

    [Header("Buttons")]
    public Button guestbtn;
    public Button loginbtn;
    public Button registerbtn;
    public Button registerCnfrmbtn;
    public Button backbtn;
    public static LoginManager Instance;

    private void Start()
    {
        AudioManager.Instance.PlaySFXAudio(AudioStates.BGmusic, true);
        guestbtn.onClick.AddListener(LoadPanel);
        loginbtn.onClick.AddListener(CheckLoginData);
        registerbtn.onClick.AddListener(OnClickRegisterBtn);
        registerCnfrmbtn.onClick.AddListener(OnCheckUserRegistrationData);
        backbtn.onClick.AddListener(() =>
        {
            MainMenuPanel.SetActive(false);
            logInPanel.SetActive(true);
        });
        if(Instance==null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        CheckLogInStatus();
    }

    private void CheckLogInStatus()
    {
        if (PlayerPrefs.HasKey("HasLogged") && PlayerPrefs.GetInt("HasLogged")==1)
            LoadPanel();
    }

    public void LoadPanel()
    {
        //AudioManager.Instance.PlaySFXAudio(AudioStates.ButtonClick);
        logInPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        PlayerPrefs.SetInt("HasLogged", 1);
    }

    public void CheckLoginData()
    {
        var userData = PlayerPrefs.GetString("RegisterData");
        var registerObj = JsonUtility.FromJson<RegisterData>(userData);

        if (registerObj.gmail == Logingmail.text && registerObj.password == LoginPassword.text)
        {
            LoadPanel();
        }
        else
        {
            Debug.Log("Invalid Input");
        }
    }

    public void OnClickRegisterBtn()
    {
        RegisterPanel.SetActive(true);
    }

    public void OnCheckUserRegistrationData()
    {
        if (string.IsNullOrEmpty(userName.text) && string.IsNullOrEmpty(gmail.text))
        {
            Debug.Log("enter UserName");
            return;
        }

        if (string.IsNullOrEmpty(password.text) && string.IsNullOrEmpty(confirmPassword.text))
        {
            Debug.Log("enter password");
            return;
        }
        if(string.IsNullOrEmpty(gmail.text) && string.IsNullOrEmpty(gmail.text))
        {
            Debug.Log("enter the email");
            return;
        }

        SaveUserDataInPlayerPref();
    }

    private void SaveUserDataInPlayerPref()
    {
        RegisterData RegisterData = new RegisterData();

        RegisterData.userName = userName.text;
        RegisterData.gmail = gmail.text;
        RegisterData.password = password.text;
        RegisterData.confirmPassword = confirmPassword.text;

        var jsonString = JsonUtility.ToJson(RegisterData);
        PlayerPrefs.SetString("RegisterData", jsonString);
    }


   

}

public struct RegisterData
{
    public string userName;
    public string gmail;
    public string password;
    public string confirmPassword;
}