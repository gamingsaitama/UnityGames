using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    public TMP_InputField JoinRoomField;
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
        PhotonNetwork.LeaveLobby();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToHome()
    {
        PausePanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu Scene");
    }

    public void ShowRoomNumber(string rNo)
    {
        ShowRoomField.text = rNo;
    }

    public string GetRoomNumber()
    {
        return JoinRoomField.text;
    }
}
