using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private TMP_InputField JoinRoomField;
    [SerializeField] private TextMeshProUGUI ShowRoomField;
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        PausePanel.SetActive(false);
        PPGameModeManager.Instance.LeaveRoomAndLobby();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToHome()
    {
        PausePanel.SetActive(false);
        PPGameModeManager.Instance.LeaveRoomAndLobby();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu Scene");
    }

    public void ShowRoomNumber(string rNo)
    {
        ShowRoomField.text = "Room Number: "+rNo;
    }

    public void DisplayMessage(string message)
    {
        JoinRoomField.gameObject.SetActive(true);
        ShowRoomField.text = message;
    }

    public string GetRoomNumber()
    {
        return JoinRoomField.text;
    }

    public void RemoveMessage()
    {
        ShowRoomField.text = "";
    }
}
