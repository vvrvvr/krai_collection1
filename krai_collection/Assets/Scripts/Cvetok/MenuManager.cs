using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace krai_cvetok
{
    public class MenuManager : MonoBehaviour
    {
        private bool isPause;
        [SerializeField] GameObject pauseScreen;
        [SerializeField] GameObject gameScreen;
        public static System.Action OnPauseMenuManager;

        [SerializeField] private Text resumeText;
        [SerializeField] private Text restartText;
        [SerializeField] private Text exitText;
        private bool isRussian;
        [SerializeField] private GameObject endingSingleton;

        private void OnEnable()
        {
            FirstPersonAIO.OnPauseEvent += Pause;
        }

        private void OnDisable()
        {
            FirstPersonAIO.OnPauseEvent -= Pause;
        }


        void Start()
        {
            isRussian = Endings.Singleton.isRussian;
            pauseScreen.SetActive(false);
            gameScreen.SetActive(true);
            if(isRussian)
            {
                resumeText.text = "продолжить";
                restartText.text = "заново";
                exitText.text = "выход";
            }
            else
            {
                resumeText.text = "resume";
                restartText.text = "restart";
                exitText.text = "exit";
            }
        }

        public void PauseGame()
        {
            isPause = true;
            pauseScreen.SetActive(true);
            gameScreen.SetActive(false);
            //Debug.Log("pause");
            Time.timeScale = 0f;
            //SoundManager.Singleton.PauseGame();
        }

        public void ResumeGame()
        {
            isPause = false;
            pauseScreen.SetActive(false);
            gameScreen.SetActive(true);
            //Debug.Log("resume");
            Time.timeScale = 1f;
            //SoundManager.Singleton.ResumeGame();
        }
        public void ResumeGameButton()
        {
            isPause = false;
            Cursor.visible = false;
            pauseScreen.SetActive(false);
            gameScreen.SetActive(true);
            OnPauseMenuManager?.Invoke(); // pause event in firstPersonAIO script
            //Debug.Log("resume");
            Time.timeScale = 1f;
            //SoundManager.Singleton.ResumeGame();
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("flower_game");
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
