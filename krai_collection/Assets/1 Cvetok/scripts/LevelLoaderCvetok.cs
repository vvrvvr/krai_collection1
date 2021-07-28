using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoaderCvetok : MonoBehaviour
{
    [SerializeField] private Endings ending;
    [SerializeField] private GameObject loadingScreen;
    public Slider slider;

    IEnumerator LoadAsyncr(string index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
    public void StartEnglish()
    {
        ending.isRussian = false;
        StartCoroutine(LoadAsyncr("flower_game"));
    }
    public void StartRussian()
    {
        ending.isRussian = true;
        StartCoroutine(LoadAsyncr("flower_game"));
    }
}
