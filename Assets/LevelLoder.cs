using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LevelLoder : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;

   public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(loadAsynchronously(sceneIndex));
    }
    IEnumerator loadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingscreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
