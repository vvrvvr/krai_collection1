using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    public Slider slider;

    private void Awake()
    {
        Time.timeScale = 1f;
        
        var endSingletone = GameObject.FindGameObjectsWithTag("delete");
        if (endSingletone != null)
        {
            foreach (var item in endSingletone)
            {
                Destroy(item);
            }
        }
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsyncr(sceneIndex));
    }

    IEnumerator LoadAsyncr (int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

}
