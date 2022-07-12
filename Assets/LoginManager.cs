using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainmenuPanel;
    public GameObject GoogleSigninPanel;
    //public GameObject GoogleLoginContainer;
    public GameObject GoogleSignUpPanel;

    [Header("RegisterInputFields")]
    [SerializeField] InputField userName;
    [SerializeField] InputField gmail;
    [SerializeField] InputField password;
    [SerializeField] InputField confirmpassword;

    RegisterData RegisterData = new RegisterData();

    private void Start()
    {
        Debug.Log(RegisterData.userNames);
    }

    public void LoadPanel()
    {
        MainmenuPanel.SetActive(true);

       // RegisterData.gmail == 
    }

    public void OnGoogleLogin()
    {
        GoogleSigninPanel.SetActive(true);
    }

    public void OnGoogleRegister()
    {
        GoogleSignUpPanel.SetActive(true);
    }

    public void OnRegisterSetData()
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
        
       

        RegisterUserData();
    }

    private void RegisterUserData()
    {
        RegisterData.userNames = userName.ToString();
        RegisterData.gmails = gmail.ToString();
        RegisterData.passCode = password.ToString();
        RegisterData.confirmPasscode = confirmpassword.ToString();

        PlayerPrefs.SetString("RegisterData", RegisterData.ToString());
        

       

    }
}

public struct RegisterData
{
    public string userNames;
    public string gmails;
    public string passCode;
    public string confirmPasscode;
   // public string contactNumber;
}