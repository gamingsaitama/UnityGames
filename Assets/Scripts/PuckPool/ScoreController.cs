using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI Player1Text;
    [SerializeField] private TextMeshProUGUI Player2Text;
    public static ScoreController Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Player1Scored(int _Player1Score)
    {
        Player1Text.text = _Player1Score.ToString();
    }

    public void Player2Scored(int _Player2Score)
    {
        Player2Text.text = _Player2Score.ToString();
    }
}
