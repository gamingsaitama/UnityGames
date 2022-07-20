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

    [Header("RegisterInputFields")]
    [SerializeField] InputField userName;
    [SerializeField] InputField gmail;
    [SerializeField] InputField password;
    [SerializeField] InputField confirmpassword;

    [Header("LogInInputs")]
    [SerializeField] TextMeshProUGUI LoginGmail;
    [SerializeField] TextMeshProUGUI LoginPassCode;

    [Header("Buttons")]
    public Button guestbtn;
    public Button loginbtn;
    public Button registerbtn;
    public Button registerCnfrmbtn;
    public Button backbtn;

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
        });
    }

    public void LoadPanel()
    {
        AudioManager.Instance.PlaySFXAudio(AudioStates.ButtonClick);
        MainMenuPanel.SetActive(true);

    }

    public void CheckLoginData()
    {
        var userData = PlayerPrefs.GetString("RegisterData");
        var registerObj = JsonUtility.FromJson<RegisterData>(userData);
       
        if (registerObj.email == LoginGmail.text && registerObj.password == LoginPassCode.text)
        {
            MainMenuPanel.SetActive(true);
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

        if (string.IsNullOrEmpty(password.text) && string.IsNullOrEmpty(confirmpassword.text))
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

        RegisterData.userNames = userName.ToString();
        RegisterData.email = gmail.ToString();
        RegisterData.password = password.ToString();
        RegisterData.confirmPasscode = confirmpassword.ToString();

        var jsonString = JsonUtility.ToJson(RegisterData);
        PlayerPrefs.SetString("RegisterData", jsonString);
    }
}



public struct RegisterData
{
    public string userNames;
    public string email;
    public string password;
    public string confirmPasscode;
}