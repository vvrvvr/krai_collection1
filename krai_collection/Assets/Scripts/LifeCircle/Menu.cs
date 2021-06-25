using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace krai_room
{
    public class Menu : MonoBehaviour
    {
        [SerializeField]
        private GameObject settingsWindow;

        [SerializeField]
        private PlayableDirector startCatScene;
        public void Play()
        {
            startCatScene.Play();
        }

        public void OpenSettingsWindow()
        {
            settingsWindow.SetActive(true);
        }
        public void QuitTheGame()
        {
            //Application.Quit();
            SceneManager.LoadScene("main menu");
        }
        public void LoadScene()
        {
            SceneManager.LoadScene("room_GameScene");
        }
    }
}