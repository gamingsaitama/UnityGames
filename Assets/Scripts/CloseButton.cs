using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button CloseBtn;
    
    void Start()
    {
        CloseBtn.onClick.AddListener(OnClickedClosebtn);
    }

    public void OnClickedClosebtn()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu Scene");
        //LoginManager.Instance.LoadPanel();
    }
}
