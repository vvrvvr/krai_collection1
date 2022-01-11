using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;
    public static MainMenu Instance => instance;
    

    private void OnLevelWasLoaded(int level)
    {
        MainMenu[] _go = FindObjectsOfType<MainMenu>();
        for (int i = 0; i < _go.Length; i++)
            if (_go.Length > 1 && i == 0)
                Destroy(_go[i].gameObject);
    }


    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("video", 0);
        //Application.Quit();
        Time.timeScale = 1f;
        SceneManager.LoadScene("main menu");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void Pause()
    {
        if (Time.timeScale < 1)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
