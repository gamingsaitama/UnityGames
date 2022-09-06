using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI Player1Text;
    [SerializeField] private TextMeshProUGUI Player2Text;
    public static ScoreController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Player1Scored(int _Player1Score)
    {
        Player1Text.text = _Player1Score.ToString();
        if (_Player1Score >= 5)
        {
            Player1Text.text = "Won";
            Player2Text.text = "Lost";
            Invoke("RestartGame",1f);
        }
    }

    public void Player2Scored(int _Player2Score)
    {
        Player2Text.text = _Player2Score.ToString();
        if (_Player2Score>=5)
        {
            Player1Text.text = "Lost";
            Player2Text.text = "Won";
            Invoke("RestartGame", 1f);
        }
    }

    private void RestartGame()
    {
        UIManager.Instance.RestartGame();
    }
}
