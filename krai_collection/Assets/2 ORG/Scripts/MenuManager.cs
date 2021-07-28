using UnityEngine;
using UnityEngine.SceneManagement;
using krai_shooter;

namespace krai_shooter
{
    public class MenuManager : MonoBehaviour
    {
        private bool isPause;
        [SerializeField] GameObject pauseScreen;
        [SerializeField] GameObject gameScreen;

        private void OnEnable()
        {
            PlayerController.OnPauseEvent += Pause;
        }

        private void OnDisable()
        {
            PlayerController.OnPauseEvent -= Pause;
        }


        void Start()
        {
            pauseScreen.SetActive(false);
            gameScreen.SetActive(true);
        }

        public void PauseGame()
        {
            isPause = true;
            pauseScreen.SetActive(true);
            gameScreen.SetActive(false);
            Debug.Log("pause");
            Time.timeScale = 0f;
            SoundManager.Singleton.PauseGame();
        }

        public void ResumeGame()
        {
            isPause = false;
            pauseScreen.SetActive(false);
            gameScreen.SetActive(true);
            Debug.Log("resume");
            Time.timeScale = 1f;
            SoundManager.Singleton.ResumeGame();
        }
        public void ResumeGameButton()
        {
            isPause = false;
            Cursor.visible = false;
            pauseScreen.SetActive(false);
            gameScreen.SetActive(true);
            PlayerController.Singleton.controllerPauseState = !PlayerController.Singleton.controllerPauseState;
            Debug.Log("resume");
            Time.timeScale = 1f;
            SoundManager.Singleton.ResumeGame();
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            //#if UNITY_EDITOR
            //            UnityEditor.EditorApplication.isPlaying = false;
            //#else
            //             Application.Quit();
            //#endif
            Time.timeScale = 1f;
            SceneManager.LoadScene("main menu");
        }

        private void Pause()
        {
            if (!isPause)
                PauseGame();
            else
                ResumeGame();
        }
    }
}
