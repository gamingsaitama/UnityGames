using RecInfo.Game.Pingpong.Ball;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button CloseBtn;
    public Button Backbtn;
    public Button Restartbtn;

    void Start()
    {
        CloseBtn.onClick.AddListener(OnClickedClosebtn);
        Backbtn.onClick.AddListener(OnClickedClosebtn);
        Restartbtn.onClick.AddListener(ResetGame);
       
    }

    public void OnClickedClosebtn()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu Scene");
    }

   

    public void ResetGame()
    {
        SceneManager.LoadScene("PingPong");
        PGoals.Instance.ResetAll();
        Player2LifeCounter.Instance.ResetPlayer2Health();
        Player1LifeCounter.Instance.ResetPlayer1Health();
        PScoreViewItem.Instance.ScoreReset();
        Time.timeScale = 1;
    }

}
