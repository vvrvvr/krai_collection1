using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace krai_menu
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        public Slider slider;

        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsyncr(sceneIndex));
            Cursor.visible = true;
        }

        IEnumerator LoadAsyncr(int index)
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

    }
}
